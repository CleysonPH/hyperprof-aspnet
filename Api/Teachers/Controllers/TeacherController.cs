using HyperProf.Api.Teachers.Dtos;
using HyperProf.Api.Teachers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Teachers.Controllers;

[Route("api/professores")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] TeacherRequest teacherRequest)
    {
        var teacherResponse = _teacherService.Create(teacherRequest);
        return Created("", teacherResponse);
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string? q)
    {
        var teacherResponses = _teacherService.Search(q);
        return Ok(teacherResponses);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var teacherResponse = _teacherService.GetById(id);
        return Ok(teacherResponse);
    }

    [HttpDelete]
    [Authorize]
    public IActionResult Delete()
    {
        _teacherService.Delete();
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    public IActionResult Update([FromBody] TeacherRequest teacherRequest)
    {
        var teacherResponse = _teacherService.Update(teacherRequest);
        return Ok(teacherResponse);
    }
}