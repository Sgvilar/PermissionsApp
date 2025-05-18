using Microsoft.EntityFrameworkCore;
using PermissionsApp.Application.Interfaces;
using PermissionsApp.Domain;
using PermissionsApp.Data;

public class PermissionRepository : IPermissionRepository
{
    private readonly PermissionsDbContext _context;

    public PermissionRepository(PermissionsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .Include(p => p.PermissionType)
            .ToListAsync();
    }

}
