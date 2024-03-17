using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyFoodTracker.Api.Models;

namespace MyFoodTracker.Api.Controllers;

[Route("api/[controller]")]
public class FoodController : Controller
{
    private readonly ILogger<FoodController> _logger;
    private readonly IFoodRepository _foodRepository;
    private readonly IValidator<FoodItemRequest> _validator;

    public FoodController(ILogger<FoodController> logger, IFoodRepository foodRepository, IValidator<FoodItemRequest> validator)
    {
        _logger = logger;
        _foodRepository = foodRepository;
        _validator = validator;
    }
    
    [HttpGet]
    public IActionResult Food()
    {
        _logger.LogInformation($"Getting food");
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
                Property = x.PropertyName, Message = x.ErrorMessage, 
            }));
        }
        var newFoodItem = new FoodItem
        {
            Name = request.Name,
            NutritionalInfo =
            {
                Protein = request.Protein,
                Carbohydrate = request.Carbs,
                Fat = request.Fat,
                Calories = request.Calories
            }
        };
        
        _foodRepository.Add(newFoodItem);

        return Ok();
    }
}