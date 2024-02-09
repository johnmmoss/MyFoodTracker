namespace MyFoodTracker.Api;

public interface IFoodRepository
{
    IList<FoodItem> GetAll();
    Task Add(FoodItem item);
    FoodItem Get(string name);
}