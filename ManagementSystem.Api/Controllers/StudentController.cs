using ManagementSystem.Api.DTOs.Students;
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
    public ActionResult<List<StudentResponse>> GetAll()
    {
        var students = _studentService.GetAll();

        var response = students.Select(student => new StudentResponse
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Department = student.Department
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public ActionResult<StudentResponse> GetById(int id)
    {
        var student = _studentService.GetById(id);

        if (student is null)
        {
            return NotFound();
        }

        var response = new StudentResponse
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            Department = student.Department
        };

        return Ok(response);
    }

    [HttpPost]
    public ActionResult<StudentResponse> Create(
        CreateStudentRequest request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Department = request.Department
        };

        var createdStudent = _studentService.Create(student);

        var response = new StudentResponse
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Email = createdStudent.Email,
            Age = createdStudent.Age,
            Department = createdStudent.Department
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response
        );
    }

    [HttpPut("{id:int}")]
    public ActionResult<StudentResponse> Update(
        int id,
        UpdateStudentRequest request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Department = request.Department
        };

        var updatedStudent = _studentService.Update(id, student);

        if (updatedStudent is null)
        {
            return NotFound();
        }

        var response = new StudentResponse
        {
            Id = updatedStudent.Id,
            Name = updatedStudent.Name,
            Email = updatedStudent.Email,
            Age = updatedStudent.Age,
            Department = updatedStudent.Department
        };

        return Ok(response);
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