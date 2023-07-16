using FluentValidation;
using HyperProf.Api.Teachers.Dtos;

namespace HyperProf.Api.Teachers.Validators;

public class ProfileImageRequestValidator : AbstractValidator<ProfileImageRequest>
{
    public ProfileImageRequestValidator()
    {
        RuleFor(x => x.Foto)
            .NotNull()
            .WithMessage("é necessário enviar uma imagem");

        When(x => x.Foto != null, () =>
        {
            RuleFor(x => x.Foto)
                .Must(x => x.ContentType.Contains("image"))
                .WithMessage("o arquivo enviado não é uma imagem")
                .Must(x => x.Length <= 2 * 1024 * 1024)
                .WithMessage("deve ser enviado uma imagem com tamanho máximo de 2MB");
        });
    }
}