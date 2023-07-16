using HyperProf.Api.Teachers.Dtos;

namespace HyperProf.Api.Teachers.Services;

public interface IProfileImageService
{
    void UpdateProfileImage(ProfileImageRequest profileImageRequest);
}