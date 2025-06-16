using SchoolMedical.Repositories.HoaLQ;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Services.HoaLQ;

public interface ISystemUserAccountService
{
    Task<SystemUserAccount?> GetUserByUserNameAndPassword(string userName, string password);
}
public class SystemUserAccountService: ISystemUserAccountService
{
    private readonly SystemUserAccountRepository _systemUserAccountRepository;
    public SystemUserAccountService() => _systemUserAccountRepository ??= new SystemUserAccountRepository();
    
    public async Task<SystemUserAccount?> GetUserByUserNameAndPassword(string userName, string password)
    {
        var user = await _systemUserAccountRepository.GetUserByUserNameAndPassword(userName, password);
        return user;
    }
}