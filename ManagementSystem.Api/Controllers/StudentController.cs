using ManagementSystem.Api.Models;
using ManagementSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public ActionResult<List<Student>> GetAll()
    {
        var students = _studentService.GetAll();

        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = _studentService.GetById(id);

        if (student is null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create(Student student)
    {
        var createdStudent = _studentService.Create(student);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdStudent.Id },
            createdStudent
        );
    }

    [HttpPut("{id:int}")]
    public ActionResult<Student> Update(
        int id,
        Student student)
    {
        var updatedStudent = _studentService.Update(id, student);

        if (updatedStudent is null)
        {
            return NotFound();
        }

        return Ok(updatedStudent);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _studentService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}