using Microsoft.EntityFrameworkCore;
using SchoolMedical.Repositories.HoaLQ.Basic;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Repositories.HoaLQ;

public class StudentHoaLQRepository: GenericRepository<StudentsHoaLq>
{
    public StudentHoaLQRepository(){}

    public StudentHoaLQRepository(SU25_PRN222_SE1709_G1_SchoolMedicalContext context) => _context = context;

    public async Task<List<StudentsHoaLq>> GetAllAsync()
    {
        return await _context.StudentsHoaLqs.AsNoTracking().ToListAsync();
    }
}