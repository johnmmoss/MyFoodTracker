namespace MyFoodTracker.Data.FileSystem;

public class MyFoodTrackerSettings
{
    public const string Name = nameof(MyFoodTrackerSettings);

    public string DataFilePath { get; set; } = string.Empty;
}