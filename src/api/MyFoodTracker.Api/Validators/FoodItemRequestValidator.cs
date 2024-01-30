using FluentValidation;
using MyFoodTracker.Api.Models;

namespace MyFoodTracker.Api.Validators;

public class FoodItemRequestValidator : AbstractValidator<FoodItemRequest>
{

    public FoodItemRequestValidator()
    {
        RuleFor(x => x.Carbohydrate)
            .Must(x => decimal.TryParse(x, out _)).WithMessage("Must be a decimal");
    }
}