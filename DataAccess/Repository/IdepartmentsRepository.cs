using DataAccess.Model;

namespace DataAccess.Repository;

public interface IdepartmentsRepository
{
    Task<IEnumerable<departments>> GetAllAsync();
    Task<departments?> GetByIdAsync(int id);
    Task<departments> CreateAsync(departments department);
    Task<departments?> UpdateAsync(int id, departments department);
    Task<bool> DeleteAsync(int id);
}
