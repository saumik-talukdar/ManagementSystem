using ManagementSystem.Api.Data;
using ManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> AddAsync(Student student)
    {
        _dbContext.Students.Add(student);

        await _dbContext.SaveChangesAsync();

        return student;
    }

    public async Task<Student?> UpdateAsync(
        int id,
        Student student)
    {
        var existingStudent = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (existingStudent is null)
        {
            return null;
        }

        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
        existingStudent.Age = student.Age;
        existingStudent.Department = student.Department;

        await _dbContext.SaveChangesAsync();

        return existingStudent;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student is null)
        {
            return false;
        }

        _dbContext.Students.Remove(student);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}