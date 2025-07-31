using SchoolMedical.Repositories.HoaLQ;
using SchoolMedical.Repositories.HoaLQ.Helper;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Services.HoaLQ;

public interface IHealthProfilesHoaLqService
{
    Task<List<HealthProfilesHoaLq>> GetAllAsync();
    Task<HealthProfilesHoaLq> GetByIdAsync(int id);
    Task<PaginatedList<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int? weight, int? height, bool? sex, int pageNumber, int pageSize);
    Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex);
    Task<int> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq);
    Task<int> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq);
    Task<bool> RemoveAsync(int id);
}
public class HealthProfilesHoaLqService: IHealthProfilesHoaLqService
{
    private readonly IUnitOfWork _unitOfWork;
    public HealthProfilesHoaLqService()
    {
        _unitOfWork  ??= new UnitOfWork();
    }

    public async Task<List<HealthProfilesHoaLq>> GetAllAsync()
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.GetAllAsync();
    }
    
    public async Task<HealthProfilesHoaLq> GetByIdAsync(int id)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.GetByIdAsync(id);
    }
    
    
    
    public async Task<PaginatedList<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int? weight, int? height, bool? sex, int pageNumber, int pageSize)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.SearchAsync(bloodType, studentName, weight, height, sex, pageNumber, pageSize);
    }

    public async Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.FilterBySexAsync(sex);
    }
    
    public async Task<int> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
    {
        var createdProfile = await _unitOfWork.HealthProfilesHoaLqRepository.CreateAsync(healthProfilesHoaLq);
        Console.WriteLine(createdProfile);
        return createdProfile;
    }
    
    public async Task<int> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.UpdateAsync(healthProfilesHoaLq);
    }
    
    public async Task<bool> RemoveAsync(int id)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.RemoveAsync(id);
    }
}