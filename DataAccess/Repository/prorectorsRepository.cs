using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class prorectorsRepository : IprorectorsRepository
{
    private readonly AppDbContext _context;

    public prorectorsRepository(AppDbContext context)
    {
   _context = context;
    }

    public async Task<IEnumerable<prorectors>> GetAllAsync()
    {
  return await _context.prorectors.ToListAsync();
    }

    public async Task<prorectors?> GetByIdAsync(int id)
    {
    return await _context.prorectors.FindAsync(id);
    }

    public async Task<prorectors> CreateAsync(prorectors prorector)
    {
        _context.prorectors.Add(prorector);
        await _context.SaveChangesAsync();
        return prorector;
    }

    public async Task<prorectors?> UpdateAsync(int id, prorectors prorector)
    {
        var existing = await _context.prorectors.FindAsync(id);
  if (existing == null) return null;

     existing.first_name = prorector.first_name;
        existing.last_name = prorector.last_name;
        existing.email = prorector.email;
        existing.department_id_1 = prorector.department_id_1;
        existing.department_id_2 = prorector.department_id_2;

     await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var prorector = await _context.prorectors.FindAsync(id);
   if (prorector == null) return false;

        _context.prorectors.Remove(prorector);
        await _context.SaveChangesAsync();
        return true;
    }
}
