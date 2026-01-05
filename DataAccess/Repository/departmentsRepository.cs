using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class departmentsRepository : IdepartmentsRepository
{
    private readonly AppDbContext _context;

    public departmentsRepository(AppDbContext context)
    {
 _context = context;
    }

    public async Task<IEnumerable<departments>> GetAllAsync()
    {
        return await _context.departments.ToListAsync();
    }

    public async Task<departments?> GetByIdAsync(int id)
    {
        return await _context.departments.FindAsync(id);
    }

    public async Task<departments> CreateAsync(departments department)
    {
        _context.departments.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<departments?> UpdateAsync(int id, departments department)
    {
      var existing = await _context.departments.FindAsync(id);
        if (existing == null) return null;

        existing.department_name = department.department_name;

        await _context.SaveChangesAsync();
        return existing;
    }

 public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.departments.FindAsync(id);
     if (department == null) return false;

    _context.departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }
}
