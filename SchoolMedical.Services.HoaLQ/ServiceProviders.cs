namespace SchoolMedical.Services.HoaLQ;

public interface IServiceProviders
{
    HealthProfilesHoaLqService HealthProfilesHoaLqService { get; }
    StudentHoaLQService StudentHoaLqService { get; }
    SystemUserAccountService SystemUserAccountService { get; }
}

public class ServiceProviders: IServiceProviders
{
    private HealthProfilesHoaLqService _healthProfilesHoaLqService;
    private StudentHoaLQService _studentHoaLqService;
    private SystemUserAccountService _systemUserAccountService;

    public ServiceProviders()
    {
    }

    public HealthProfilesHoaLqService HealthProfilesHoaLqService
    {
        get {return _healthProfilesHoaLqService ??= new HealthProfilesHoaLqService();}
    }
    public StudentHoaLQService StudentHoaLqService
    {
        get {return _studentHoaLqService ??= new StudentHoaLQService();}
    }
    public SystemUserAccountService SystemUserAccountService
    {
        get {return _systemUserAccountService ??= new SystemUserAccountService();}
    }
}