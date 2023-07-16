using FluentValidation;
using HyperProf.Api.Teachers.Dtos;
using HyperProf.Core.Services.Authentication;
using HyperProf.Core.Services.Storage;
using HyperProf.Core.UOW;

namespace HyperProf.Api.Teachers.Services;

public class ProfileImageService : IProfileImageService
{
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<ProfileImageRequest> _profileImageRequestValidator;
    private readonly IHyperprofAuthenticationService _hyperprofAuthenticationService;

    public ProfileImageService(
        IStorageService storageService,
        IUnitOfWork uow,
        IValidator<ProfileImageRequest> profileImageRequestValidator,
        IHyperprofAuthenticationService hyperprofAuthenticationService)
    {
        _storageService = storageService;
        _uow = uow;
        _profileImageRequestValidator = profileImageRequestValidator;
        _hyperprofAuthenticationService = hyperprofAuthenticationService;
    }

    public void UpdateProfileImage(ProfileImageRequest profileImageRequest)
    {
        _profileImageRequestValidator.ValidateAndThrow(profileImageRequest);
        var teacher = _hyperprofAuthenticationService.GetAuthenticatedUser();
        var profileImage = _storageService.UploadFile(
            profileImageRequest.Foto.FileName,
            profileImageRequest.Foto.OpenReadStream(),
            profileImageRequest.Foto.ContentType);
        teacher.ProfilePicture = profileImage;
        _uow.Teachers.Update(teacher);
        _uow.SaveChanges();
    }
}