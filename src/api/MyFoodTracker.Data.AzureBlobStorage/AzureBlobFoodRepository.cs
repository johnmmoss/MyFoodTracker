using Azure.Storage.Blobs;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MyFoodTracker.Api;
using Exception = System.Exception;

namespace MyFoodTracker.Data.AzureBlobStorage;

public class AzureBlobFoodRepository : IFoodRepository
{
    private const string ContainerName = "main";
    private const string FileName = "my-food-tracker.json";

    private readonly ILogger<AzureBlobFoodRepository> _logger;
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobFoodRepository(ILogger<AzureBlobFoodRepository> logger, BlobServiceClient blobServiceClient)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
    }
    public IList<FoodItem> GetAll()
    {
        _logger.LogInformation("Getting food items");
        return GetCurrentItems();      
    }

    public async Task Add(FoodItem item)
    {
        _logger.LogInformation($"Adding new food item {item.Name}");
        var currentItems = GetCurrentItems();
        currentItems.Add(item);
        var newContent = JsonSerializer.Serialize(currentItems);
        var blobClient = GetBlobClient(ContainerName, FileName);
        await blobClient.UploadAsync(BinaryData.FromString(newContent), overwrite: true);
    }

    public FoodItem Get(string name)
    {
        var currentItems = GetCurrentItems();
        var result = currentItems.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        return result;
    }

    private List<FoodItem> GetCurrentItems()
    {
        try
        {
            var blobClient = GetBlobClient(ContainerName, FileName);
            if (!blobClient.Exists())
            {
                blobClient.Upload(BinaryData.FromString("[]"));
                return new List<FoodItem>(); 
            }
            var data = blobClient.DownloadContent().Value.Content;
            return data != null ? JsonSerializer.Deserialize<List<FoodItem>>(data) : new List<FoodItem>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There was a problem getting the current items.");
            throw;
        }
    }

    private BlobClient GetBlobClient(string containerName, string fileName)
    {
        return _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(FileName);
    }
}