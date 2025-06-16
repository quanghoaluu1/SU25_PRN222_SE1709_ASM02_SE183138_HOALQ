using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMedical.Repositories.HoaLQ.Basic;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Repositories.HoaLQ
{
    public class HealthProfilesHoaLqRepository: GenericRepository<HealthProfilesHoaLq>
    {
        private readonly IUnitOfWork _unitOfWork;
        public HealthProfilesHoaLqRepository()
        {
            _context ??= new SU25_PRN222_SE1709_G1_SchoolMedicalContext();
        }

        public HealthProfilesHoaLqRepository(SU25_PRN222_SE1709_G1_SchoolMedicalContext context)
        {
            _context = context;
        }
        
        public new async Task< List<HealthProfilesHoaLq>> GetAllAsync()
        {
            return await _context.HealthProfilesHoaLqs
                .Include(h => h.Student)
                .ToListAsync() ?? new List<HealthProfilesHoaLq>();
        }

        public async Task<HealthProfilesHoaLq> GetByIdAsync(int id)
        {
            var healthProfile = await _context.HealthProfilesHoaLqs
                .AsNoTracking()
                .Include(h => h.Student)
                .FirstOrDefaultAsync(h => h.HealthProfileHoaLqid == id);
            return healthProfile ?? new HealthProfilesHoaLq();
        }

        public async Task<List<HealthProfilesHoaLq>> SearchAsync(string bloodType, string searchCode, int sight)
        {
            var healthProfiles = await _context.HealthProfilesHoaLqs.Include(h => h.Student).Where(h => (h.BloodType.Contains(bloodType) || string.IsNullOrEmpty(bloodType))
            && (h.Student.StudentCode.Contains(searchCode) || string.IsNullOrEmpty(searchCode))
            && (h.Sight == sight || sight == 0))
                .ToListAsync();

            return healthProfiles ?? new List<HealthProfilesHoaLq>();
        }
        
        public new async Task<HealthProfilesHoaLq> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
        {
            var entity = _context.HealthProfilesHoaLqs.Add(healthProfilesHoaLq);
            await _context.SaveChangesAsync();

            var result = await _context.HealthProfilesHoaLqs
                .Where(h => h.HealthProfileHoaLqid == entity.Entity.HealthProfileHoaLqid)
                .Select(h => new HealthProfilesHoaLq
                {
                    HealthProfileHoaLqid = h.HealthProfileHoaLqid,
                    Weight = h.Weight,
                    Height = h.Height,
                    BloodPressure = h.BloodPressure,
                    Allergy = h.Allergy,
                    ChronicDisease = h.ChronicDisease,
                    MedicalHistory = h.MedicalHistory,
                    CurrentMedical = h.CurrentMedical,
                    BloodType = h.BloodType,
                    Sight = h.Sight,
                    Hearing = h.Hearing,
                    Sex = h.Sex,
                    DateOfBirth = h.DateOfBirth,
                    StudentId = h.StudentId,
            
                    Student = new StudentsHoaLq()
                    {
                        StudentsHoaLqid = h.Student.StudentsHoaLqid,
                        StudentFullName = h.Student.StudentFullName
                    }
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public new async Task<HealthProfilesHoaLq> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
        {
            var entity = _context.HealthProfilesHoaLqs.Update(healthProfilesHoaLq);
            await _context.SaveChangesAsync();

            var result = await _context.HealthProfilesHoaLqs
                .Where(h => h.HealthProfileHoaLqid == entity.Entity.HealthProfileHoaLqid)
                .Select(h => new HealthProfilesHoaLq
                {
                    HealthProfileHoaLqid = h.HealthProfileHoaLqid,
                    Weight = h.Weight,
                    Height = h.Height,
                    BloodPressure = h.BloodPressure,
                    Allergy = h.Allergy,
                    ChronicDisease = h.ChronicDisease,
                    MedicalHistory = h.MedicalHistory,
                    CurrentMedical = h.CurrentMedical,
                    BloodType = h.BloodType,
                    Sight = h.Sight,
                    Hearing = h.Hearing,
                    Sex = h.Sex,
                    DateOfBirth = h.DateOfBirth,
                    StudentId = h.StudentId,

                    Student = new StudentsHoaLq()
                    {
                        StudentsHoaLqid = h.Student.StudentsHoaLqid,
                        StudentFullName = h.Student.StudentFullName
                    }
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
