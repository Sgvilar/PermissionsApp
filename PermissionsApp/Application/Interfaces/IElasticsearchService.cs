using PermissionsApp.Domain;

namespace PermissionsApp.Application.Interfaces
{
    public interface IElasticsearchService
    {
        Task IndexPermissionAsync(Permission permission);
    }
}
