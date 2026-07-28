using FluentValidation;
using OrderFlow.Application.DTOs.Customer;

namespace OrderFlow.Application.Validators.Customer;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is mandatory");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is mandatory")
            .EmailAddress().WithMessage("Email format invalid");
        
    }
}