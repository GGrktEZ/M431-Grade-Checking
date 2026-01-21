using Microsoft.EntityFrameworkCore;
using DataAccess.Model;

namespace DataAccess.Repository;

public class modulesRepository : ImodulesRepository
{
    private readonly AppDbContext _db;

 public modulesRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public modules? GetmoduleById(int id)
    {
return _db.modules.Find(id);
    }

    /// <inheritdoc />
    public IEnumerable<modules> GetAllmodules()
    {
        return _db.modules.ToList();
  }

    /// <inheritdoc />
  public IEnumerable<modules> GetModulesByIds(IEnumerable<int> moduleIds)
    {
     return _db.modules.Where(m => moduleIds.Contains(m.module_id)).ToList();
}
}
