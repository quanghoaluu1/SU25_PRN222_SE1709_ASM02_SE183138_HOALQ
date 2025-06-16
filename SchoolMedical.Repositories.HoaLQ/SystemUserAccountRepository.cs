using Microsoft.EntityFrameworkCore;
using SchoolMedical.Repositories.HoaLQ.Basic;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Repositories.HoaLQ;

public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
{
   public SystemUserAccountRepository(){}
   public SystemUserAccountRepository(SU25_PRN222_SE1709_G1_SchoolMedicalContext context) => _context = context;
   
   public async Task<SystemUserAccount?> GetUserByUserNameAndPassword(string userName, string password)
   {
      var user = await _context.SystemUserAccounts
         .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive);
      return user ?? null;
   } 
}