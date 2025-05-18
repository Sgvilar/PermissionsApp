using PermissionsApp.Domain;

namespace PermissionsApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IPermissionRepository Permissions { get; }
        Task CommitAsync();
        Task SaveChangesAsync();
    
    }
}
