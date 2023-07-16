using HyperProf.Api.Students.Dtos;
using HyperProf.Api.Students.Services;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Students.Controllers;

[Route("api/professores")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("{professor_id}/alunos")]
    public IActionResult Create([FromRoute(Name = "professor_id")] int teacherId, [FromBody] StudentRequest studentRequest)
    {
        var studentResponse = _studentService.Create(teacherId, studentRequest);
        return CreatedAtAction(nameof(Create), studentResponse);
    }
}