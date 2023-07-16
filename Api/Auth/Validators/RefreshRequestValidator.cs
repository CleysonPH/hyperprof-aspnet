using FluentValidation;
using HyperProf.Api.Auth.Dtos;

namespace HyperProf.Api.Auth.Validators;

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("é obrigatório");
    }
}