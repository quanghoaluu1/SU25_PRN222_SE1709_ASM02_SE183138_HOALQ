using SchoolMedical.Repositories.HoaLQ;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Services.HoaLQ;

public interface IStudentHoaLQService
{
    Task<List<StudentsHoaLq>> GetAllAsync();
}

public class StudentHoaLQService: IStudentHoaLQService
{
    private readonly StudentHoaLQRepository _studentHoaLQRepository;
    public StudentHoaLQService() => _studentHoaLQRepository ??= new StudentHoaLQRepository();
    
    public async Task<List<StudentsHoaLq>> GetAllAsync()
    {
        return await _studentHoaLQRepository.GetAllAsync();
    }
}