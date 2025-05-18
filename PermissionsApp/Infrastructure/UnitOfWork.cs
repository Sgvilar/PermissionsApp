using PermissionsApp.Application.Interfaces;
using PermissionsApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly PermissionsDbContext _context;

    public IPermissionRepository Permissions { get; }

    public IPermissionRepository PermissionsType { get; }
     
    public UnitOfWork(PermissionsDbContext context, IPermissionRepository permissionRepository)
    {
        _context = context;
        Permissions = permissionRepository;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
