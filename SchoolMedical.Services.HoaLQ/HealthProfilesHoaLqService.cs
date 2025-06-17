using SchoolMedical.Repositories.HoaLQ;
using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Services.HoaLQ;

public interface IHealthProfilesHoaLqService
{
    Task<List<HealthProfilesHoaLq>> GetAllAsync();
    Task<HealthProfilesHoaLq> GetByIdAsync(int id);
    Task<List<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int weight, int height);
    Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex);
    Task<HealthProfilesHoaLq> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq);
    Task<HealthProfilesHoaLq> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq);
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
    
    public async Task<List<HealthProfilesHoaLq>> SearchAsync(string bloodType, string studentName, int weight, int height)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.SearchAsync(bloodType, studentName, weight, height);
    }

    public async Task<List<HealthProfilesHoaLq>> FilterBySexAsync(bool? sex)
    {
        return await _unitOfWork.HealthProfilesHoaLqRepository.FilterBySexAsync(sex);
    }
    
    public async Task<HealthProfilesHoaLq> CreateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
    {
        var createdProfile = await _unitOfWork.HealthProfilesHoaLqRepository.CreateAsync(healthProfilesHoaLq);
        Console.WriteLine(createdProfile);
        return createdProfile;
    }
    
    public async Task<HealthProfilesHoaLq> UpdateAsync(HealthProfilesHoaLq healthProfilesHoaLq)
    {
        var updatedProfile = await _unitOfWork.HealthProfilesHoaLqRepository.UpdateAsync(healthProfilesHoaLq);
        return updatedProfile;
    }
    
    public async Task<bool> RemoveAsync(int id)
    {
        var removedProfile = await _unitOfWork.HealthProfilesHoaLqRepository.GetByIdAsync(id);
        return await _unitOfWork.HealthProfilesHoaLqRepository.RemoveAsync(removedProfile);
    }
}