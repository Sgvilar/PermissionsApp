using PermissionsApp.Domain;

namespace PermissionsApp.Application.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllAsync();
       
    }
}
