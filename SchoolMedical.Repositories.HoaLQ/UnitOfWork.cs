using SchoolMedical.Repositories.HoaLQ.Models;

namespace SchoolMedical.Repositories.HoaLQ;

public interface IUnitOfWork: IDisposable
{
    HealthProfilesHoaLqRepository HealthProfilesHoaLqRepository { get; }
    StudentHoaLQRepository StudentsHoaLqRepository { get;  }
    SystemUserAccountRepository SystemUserAccountRepository { get; }
    
    int SaveChanges();
    Task<int> SaveChangesAsync();
}

public class UnitOfWork: IUnitOfWork, IAsyncDisposable
{
    private readonly SU25_PRN222_SE1709_G1_SchoolMedicalContext _context;
    private HealthProfilesHoaLqRepository _healthProfilesHoaLqRepository;
    private StudentHoaLQRepository _studentsHoaLqRepository;
    private SystemUserAccountRepository _systemUserAccountRepository;

    public UnitOfWork() => _context ??= new SU25_PRN222_SE1709_G1_SchoolMedicalContext();

    public HealthProfilesHoaLqRepository HealthProfilesHoaLqRepository
    {
        get { return _healthProfilesHoaLqRepository ??= new HealthProfilesHoaLqRepository(_context); }
    }

    public StudentHoaLQRepository StudentsHoaLqRepository
    {
        get { return _studentsHoaLqRepository ??= new StudentHoaLQRepository(_context); }
    }

    public SystemUserAccountRepository SystemUserAccountRepository
    {
        get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
    }

    public int SaveChanges()
    {
        int result = -1;
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                result = _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving changes: " + ex.Message);
                result = -1;
                transaction.Rollback();
                throw;
            }
        }

        return result;
    }

    public async Task<int> SaveChangesAsync()
    {
        int result = -1;
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                result = await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving changes: " + ex.Message);
                result = -1;
                transaction.Rollback();
                throw;
            }
        }
        return result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}