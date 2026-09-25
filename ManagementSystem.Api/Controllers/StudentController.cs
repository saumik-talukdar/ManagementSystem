
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
    public async Task<ActionResult<List<StudentResponse>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();

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
    public async Task<ActionResult<StudentResponse>> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);

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
    public async Task<ActionResult<StudentResponse>> Create(
        CreateStudentRequest request)
    {
        var student = new Student
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Department = request.Department
        };

        var createdStudent =
            await _studentService.CreateAsync(student);

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
    public async Task<ActionResult<StudentResponse>> Update(
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

        var updatedStudent =
            await _studentService.UpdateAsync(id, student);

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
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _studentService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}

