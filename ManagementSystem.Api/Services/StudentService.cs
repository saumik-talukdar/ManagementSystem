using ManagementSystem.Api.Models;

namespace ManagementSystem.Api.Services;

public class StudentService
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

    public List<Student> GetAll()
    {
        return _students;
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public Student Create(Student student)
    {
        var nextId = _students.Count == 0
            ? 1
            : _students.Max(s => s.Id) + 1;

        student.Id = nextId;

        _students.Add(student);

        return student;
    }

    public Student? Update(int id, Student updatedStudent)
    {
        var existingStudent = _students.FirstOrDefault(s => s.Id == id);

        if (existingStudent is null)
        {
            return null;
        }

        existingStudent.Name = updatedStudent.Name;
        existingStudent.Email = updatedStudent.Email;
        existingStudent.Age = updatedStudent.Age;
        existingStudent.Department = updatedStudent.Department;

        return existingStudent;
    }

    public bool Delete(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return false;
        }

        _students.Remove(student);

        return true;
    }
}