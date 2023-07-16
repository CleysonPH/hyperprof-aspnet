using FluentValidation;
using HyperProf.Api.Auth.Dtos;

namespace HyperProf.Api.Auth.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .EmailAddress()
            .WithMessage("não é um email válido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("é obrigatório");
    }
}