namespace MyFoodTracker.Api.Models;

public class FoodItemRequest
{
    public string Name { get; set; }
    public string Quantity { get; set; }
    public string Protein { get; set; }
    public string Fat { get; set; }
    public string Carbohydrate { get; set; }
    public string Fibre { get; set; }
    public string Calories { get; set; }
}