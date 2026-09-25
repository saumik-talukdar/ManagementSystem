using ManagementSystem.Api.Models;
using ManagementSystem.Api.Repositories;

namespace ManagementSystem.Api.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        return await _studentRepository.AddAsync(student);
    }

    public async Task<Student?> UpdateAsync(
        int id,
        Student student)
    {
        return await _studentRepository.UpdateAsync(id, student);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _studentRepository.DeleteAsync(id);
    }
}