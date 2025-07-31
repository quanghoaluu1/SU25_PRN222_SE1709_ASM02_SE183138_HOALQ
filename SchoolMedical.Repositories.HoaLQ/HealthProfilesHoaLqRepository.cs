using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMedical.Repositories.HoaLQ.Basic;
using SchoolMedical.Repositories.HoaLQ.Helper;
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
                .OrderByDescending(h => h.HealthProfileHoaLqid)
                .ToListAsync() ?? new List<HealthProfilesHoaLq>();
        }

        public new async Task<HealthProfilesHoaLq> GetByIdAsync(int id)
        {
            var healthProfile = await _context.HealthProfilesHoaLqs
                .AsNoTracking()
                .Include(h => h.Student)
                .FirstOrDefaultAsync(h => h.HealthProfileHoaLqid == id);
            return healthProfile ?? new HealthProfilesHoaLq();
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var local = _context.HealthProfilesHoaLqs.Local
                .FirstOrDefault(h => h.HealthProfileHoaLqid == id);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            var entityToDelete = await _context.HealthProfilesHoaLqs
                .FirstOrDefaultAsync(h => h.HealthProfileHoaLqid == id);
            if (entityToDelete == null) return false;
            _context.HealthProfilesHoaLqs.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int? weight, int? height, bool? sex, int pageNumber, int pageSize)
        {
            var query = _context.HealthProfilesHoaLqs
                .Include(p => p.Student)
                .OrderByDescending(h => h.HealthProfileHoaLqid)
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
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<HealthProfilesHoaLq>(items, totalCount);
        }

        public async Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex)
        {
                return await _context.HealthProfilesHoaLqs
                    .Include(h => h.Student)
                    .Where(h => h.Sex.Equals(sex))
                    .ToListAsync();
        }
        public new async Task<int> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
        { 
            _context.HealthProfilesHoaLqs.Add(healthProfilesHoaLq);
            return await _context.SaveChangesAsync();
            // var result = await _context.HealthProfilesHoaLqs
            //     .Include(h => h.Student) // <-- Dòng quan trọng để lấy dữ liệu Student
            //     .FirstOrDefaultAsync(h => h.HealthProfileHoaLqid == healthProfilesHoaLq.HealthProfileHoaLqid);

        }

        public new async Task<int> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
        { 
            var local = _context.HealthProfilesHoaLqs.Local
                .FirstOrDefault(entry => entry.HealthProfileHoaLqid == healthProfilesHoaLq.HealthProfileHoaLqid);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            healthProfilesHoaLq.Student = null;
            _context.HealthProfilesHoaLqs.Update(healthProfilesHoaLq);
            return await _context.SaveChangesAsync();
        }
    }
}
