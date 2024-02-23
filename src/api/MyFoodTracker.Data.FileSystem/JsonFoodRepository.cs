using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyFoodTracker.Api;

namespace MyFoodTracker.Data.FileSystem;

public class JsonFoodRepository : IFoodRepository
{
    private readonly ILogger<JsonFoodRepository> _logger;
    private readonly IFileSystem _fileSystem;
    private MyFoodTrackerSettings _settings;

    public JsonFoodRepository(ILogger<JsonFoodRepository> logger, IOptions<MyFoodTrackerSettings> settings, IFileSystem fileSystem)
    {
        _logger = logger;
        _fileSystem = fileSystem;
        _settings = settings.Value;
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
        await _fileSystem.WriteAsync(_settings.DataFilePath, newContent);
    }

    public FoodItem Get(string name)
    {
        return GetCurrentItems().First(x => x.Name.ToLower() == name.ToLower());
    }

    private List<FoodItem> GetCurrentItems()
    {
        var fileContents = _fileSystem.Read(_settings.DataFilePath);

        return fileContents != string.Empty
            ? JsonSerializer.Deserialize<List<FoodItem>>(fileContents)
            : new List<FoodItem>();
    }
}