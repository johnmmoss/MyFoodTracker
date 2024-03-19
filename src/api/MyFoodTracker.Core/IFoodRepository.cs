namespace MyFoodTracker.Api;

public interface IFoodRepository
{
    IList<FoodItem> GetAll();
    Task Add(FoodItem item);
    Task Delete(string id);
    FoodItem Get(string name);
}