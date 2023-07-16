using HyperProf.Api.Teachers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Teachers.Controllers;

[Route("api/me")]
[ApiController]
[Authorize]
public class MeController : ControllerBase
{
    private readonly IMeService _meService;

    public MeController(IMeService meService)
    {
        _meService = meService;
    }

    [HttpGet]
    public IActionResult GetMe()
    {
        var teacher = _meService.GetMe();
        return Ok(teacher);
    }
}