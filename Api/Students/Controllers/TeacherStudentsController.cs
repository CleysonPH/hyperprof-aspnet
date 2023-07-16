using HyperProf.Api.Students.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperProf.Api.Students.Controllers;

[Route("/api/professores/alunos")]
[ApiController]
[Authorize]
public class TeacherStudentsController : ControllerBase
{
    private readonly ITeacherStudentsService _teacherStudentsService;

    public TeacherStudentsController(ITeacherStudentsService teacherStudentsService)
    {
        _teacherStudentsService = teacherStudentsService;
    }

    [HttpGet]
    public IActionResult GetStudentsByAuthenticatedTeacher()
    {
        var students = _teacherStudentsService.GetStudentsByAuthenticatedTeacher();
        return Ok(students);
    }
}