using System.Text.Json;
using Microsoft.Extensions.Options;
using MyFoodTracker.Api.Settings;

namespace MyFoodTracker.Api;

public interface IFoodRepository
{
    IList<FoodItem> GetAll();
    void Add(FoodItem item);
    FoodItem Get(string name);
}

public class FoodRepository : IFoodRepository
{
    private readonly IFileSystem _fileSystem;
    private MyFoodTrackerSettings _settings;

    public FoodRepository(IOptions<MyFoodTrackerSettings> settings, IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        _settings = settings.Value;
    }
    
    public IList<FoodItem> GetAll()
    {
        return GetCurrentItems();
    }

    public void Add(FoodItem item)
    {
        var currentItems = GetCurrentItems();
        currentItems.Add(item);
        var newContent = JsonSerializer.Serialize(currentItems);
        _fileSystem.Write(_settings.DataFilePath, newContent);
    }

    public FoodItem Get(string name)
    {
        return GetCurrentItems().First(x => x.Name.ToLower() == name.ToLower());
    }

    private List<FoodItem> GetCurrentItems()
    {
        var fileContents = _fileSystem.Read(_settings.DataFilePath);

        return fileContents != string.Empty ? 
            JsonSerializer.Deserialize<List<FoodItem>>(fileContents) : 
            new List<FoodItem>();
    }
}