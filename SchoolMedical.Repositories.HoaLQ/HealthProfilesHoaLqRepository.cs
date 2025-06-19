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

        public new async Task<HealthProfilesHoaLq> GetByIdAsync(int id)
        {
            var healthProfile = await _context.HealthProfilesHoaLqs
                .Include(h => h.Student)
                .FirstOrDefaultAsync(h => h.HealthProfileHoaLqid == id);
            return healthProfile ?? new HealthProfilesHoaLq();
        }

        public async Task<List<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int? weight, int? height, bool? sex)
        {
            var query = _context.HealthProfilesHoaLqs
                .Include(p => p.Student)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(bloodType))
            {
                query = query.Where(p => p.BloodType.Contains(bloodType));
            }

            if (!string.IsNullOrWhiteSpace(studentName))
            {
                query = query.Where(p => p.Student.StudentFullName.Contains(studentName));
            }

            if (weight.HasValue)
            {
                query = query.Where(p => p.Weight == weight.Value);
            }

            if (height.HasValue)
            {
                query = query.Where(p => p.Height == height.Value);
            }
    
            if (sex.HasValue)
            {
                query = query.Where(p => p.Sex == sex.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex)
        {
                return await _context.HealthProfilesHoaLqs
                    .Include(h => h.Student)
                    .Where(h => h.Sex.Equals(sex))
                    .ToListAsync();
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
