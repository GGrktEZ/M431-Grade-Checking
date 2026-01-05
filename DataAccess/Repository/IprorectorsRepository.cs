using DataAccess.Model;

namespace DataAccess.Repository;

public interface IprorectorsRepository
{
    Task<IEnumerable<prorectors>> GetAllAsync();
    Task<prorectors?> GetByIdAsync(int id);
    Task<prorectors> CreateAsync(prorectors prorector);
    Task<prorectors?> UpdateAsync(int id, prorectors prorector);
    Task<bool> DeleteAsync(int id);
}
