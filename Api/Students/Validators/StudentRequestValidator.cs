using FluentValidation;
using HyperProf.Api.Students.Dtos;

namespace HyperProf.Api.Students.Validators;

public class StudentRequestValidator : AbstractValidator<StudentRequest>
{
    public StudentRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("é obrigatório.")
            .Length(3, 100)
            .WithMessage("deve ter entre {MinLength} e {MaxLength} caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("é obrigatório.")
            .EmailAddress()
            .WithMessage("é inválido.");

        RuleFor(x => x.DataAula)
            .NotEmpty()
            .WithMessage("é obrigatório.")
            .GreaterThan(DateTime.Now)
            .WithMessage("deve ser maior que a data atual.");
    }
}