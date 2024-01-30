using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyFoodTracker.Api.Models;

namespace MyFoodTracker.Api.Controllers;

[Route("api/[controller]")]
public class FoodController : Controller
{
    private readonly IFoodRepository _foodRepository;
    private readonly IValidator<FoodItemRequest> _validator;

    public FoodController(IFoodRepository foodRepository, IValidator<FoodItemRequest> validator)
    {
        _foodRepository = foodRepository;
        _validator = validator;
    }
    
    [HttpGet]
    public IActionResult Food()
    {
        var foodItems = _foodRepository.GetAll();
        return Ok(foodItems);
    }

    [HttpPost]
    public IActionResult Food([FromBody]FoodItemRequest request)
    {
        var valid =_validator.Validate(request);
        if (!valid.IsValid)
        {
            return BadRequest(valid.Errors.Select(x => new
            {
                Property = x.PropertyName, Error = x.ErrorMessage, 
            }));
        }
        var newFoodItem = new FoodItem();
        newFoodItem.Name = request.Name;
        newFoodItem.NutritionalInfo.Carbohydrate = decimal.Parse(request.Carbohydrate);
        newFoodItem.NutritionalInfo.Fibre = decimal.Parse(request.Fibre);
        newFoodItem.NutritionalInfo.Fat = decimal.Parse(request.Fat);
        newFoodItem.NutritionalInfo.Protein = decimal.Parse(request.Protein);
        newFoodItem.NutritionalInfo.Calories = int.Parse(request.Calories);
        
        _foodRepository.Add(newFoodItem);

        return Ok();
    }
}