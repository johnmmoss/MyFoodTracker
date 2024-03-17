using MyFoodTracker.Api.Models;
using FluentValidation;

namespace MyFoodTracker.Api.Validators;

public class FoodItemRequestValidator : AbstractValidator<FoodItemRequest>
{
    public FoodItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(x => x.Protein)
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Must be a decimal");
        
        RuleFor(x => x.Fat)
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Must be a decimal");
        
        RuleFor(x => x.Carbs)
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Must be a decimal");
        
        RuleFor(x => x.Calories)
            .Must(x => decimal.TryParse(x.ToString(), out _))
            .WithMessage("Must be a decimal");
    }
}