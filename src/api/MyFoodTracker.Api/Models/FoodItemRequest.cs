using System.Text.Json.Serialization;

namespace MyFoodTracker.Api.Models;

public class FoodItemRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("protein")]
    public decimal Protein { get; set; }
    [JsonPropertyName("fat")]
    public decimal Fat { get; set; }
    [JsonPropertyName("carbs")]
    public decimal Carbs { get; set; }
    [JsonPropertyName("calories")]
    public int Calories { get; set; }
}