using ManagementSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly List<Student> _students =
    [
        new()
        {
            Id = 1,
            Name = "Saumik",
            Email = "saumik@example.com",
            Age = 22,
            Department = "CSE"
        },
        new()
        {
            Id = 2,
            Name = "Rahim",
            Email = "rahim@example.com",
            Age = 23,
            Department = "CSE"
        }
    ];

    [HttpGet]
    public ActionResult<List<Student>> GetStudents()
    {
        return _students;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound();
        }

        return student;
    }
}