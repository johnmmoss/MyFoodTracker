namespace MyFoodTracker.Api;

public class FoodItem
{
    public string Name { get; set; } = string.Empty;

    public NutritionalInfo NutritionalInfo { get; set; } = new();
}