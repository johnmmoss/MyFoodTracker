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
        _logger.LogInformation($"Adding food {request.Name}");
        
        var valid =_validator.Validate(request);
        if (!valid.IsValid)
        {
            return BadRequest(valid.Errors.Select(x => new
            {
                Property = x.PropertyName, Error = x.ErrorMessage, 
            }));
        }
        var newFoodItem = new FoodItem
        {
            Name = request.Name,
            NutritionalInfo =
            {
                Carbohydrate = decimal.Parse(request.Carbohydrate),
                Fibre = decimal.Parse(request.Fibre),
                Fat = decimal.Parse(request.Fat),
                Protein = decimal.Parse(request.Protein),
                Calories = int.Parse(request.Calories)
            }
        };

        _foodRepository.Add(newFoodItem);

        return Ok();
    }
}