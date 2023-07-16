using FluentValidation;
using HyperProf.Api.Teachers.Dtos;
using HyperProf.Core.UOW;

namespace HyperProf.Api.Teachers.Validators;

public class TeacherRequestValidator : AbstractValidator<TeacherRequest>
{
    public TeacherRequestValidator(IUnitOfWork uow)
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .Length(3, 100)
            .WithMessage("deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .EmailAddress()
            .WithMessage("deve ser um endereço de email válido")
            .MaximumLength(255)
            .WithMessage("deve ter no máximo {MaxLength} caracteres")
            .Must((tr, x) =>
            {
                if (tr.Id == 0)
                    return !uow.Teachers.Any(t => t.Email == x);
                return !uow.Teachers.Any(t => t.Email == x && t.Id != tr.Id);
            })
            .WithMessage("já está em uso");

        RuleFor(x => x.Idade)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .InclusiveBetween(18, 100)
            .WithMessage("deve ser entre {From} e {To}");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .Length(10, 500)
            .WithMessage("deve ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .MinimumLength(6)
            .WithMessage("deve ter no mínimo {MinLength} caracteres");

        RuleFor(x => x.PasswordConfirmation)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .MinimumLength(6)
            .WithMessage("deve ter no mínimo {MinLength} caracteres")
            .Equal(x => x.Password)
            .WithMessage("as senhas devem ser iguais");

        RuleFor(x => x.ValorHora)
            .NotEmpty()
            .WithMessage("é obrigatório")
            .InclusiveBetween(10, 500)
            .WithMessage("deve ser entre {From} e {To}");
    }
}