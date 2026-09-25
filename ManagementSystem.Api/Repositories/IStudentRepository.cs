using ManagementSystem.Api.Models;

namespace ManagementSystem.Api.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(int id);

    Task<Student> AddAsync(Student student);

    Task<Student?> UpdateAsync(int id, Student student);

    Task<bool> DeleteAsync(int id);
}