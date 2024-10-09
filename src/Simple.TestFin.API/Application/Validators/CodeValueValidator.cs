using System.Collections;
using FluentValidation;
using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Application.Validators;

public class CodeValueValidator : AbstractValidator<CodeValue>
{
    public CodeValueValidator()
    {
        RuleFor(record => record.Code)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Code must be equal or greater than 0.");

        RuleFor(record => record.Value)
            .NotEmpty()
            .WithMessage("Value is required.")
            .MaximumLength(255)
            .WithMessage("Value length must be maximum of 255 characters.");
    }
}

public class CodeValueCollectionValidator : AbstractValidator<IEnumerable<CodeValue>>
{
    public CodeValueCollectionValidator()
    {
        RuleFor(d => d)
            .NotNull()
            .NotEmpty();
        
        RuleForEach(e => e).SetValidator(new CodeValueValidator());
    }
}