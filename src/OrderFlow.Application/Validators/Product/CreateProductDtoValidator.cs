using FluentValidation;
using OrderFlow.Application.DTOs.Product;

namespace OrderFlow.Application.Validators.Product;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Products name is mandatory")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
        
        RuleFor(x=> x.CategoryId)
            .NotEmpty().WithMessage("Category is mandatory");
    }
}