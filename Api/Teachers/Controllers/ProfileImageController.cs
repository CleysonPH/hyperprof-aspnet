using HyperProf.Api.Teachers.Dtos;
using HyperProf.Api.Teachers.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Teachers.Controllers;

[Route("api/professores/foto")]
[ApiController]
public class ProfileImageController : ControllerBase
{
    private readonly IProfileImageService _profileImageService;

    public ProfileImageController(IProfileImageService profileImageService)
    {
        _profileImageService = profileImageService;
    }

    [HttpPost]
    public IActionResult UpdateProfileImage([FromForm] ProfileImageRequest profileImageRequest)
    {
        _profileImageService.UpdateProfileImage(profileImageRequest);
        return NoContent();
    }
}