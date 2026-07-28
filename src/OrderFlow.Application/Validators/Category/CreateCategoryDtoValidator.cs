using FluentValidation;
using OrderFlow.Application.DTOs.Category;

namespace OrderFlow.Application.Validators.Category;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Categorys name is mandatory")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
    }
}