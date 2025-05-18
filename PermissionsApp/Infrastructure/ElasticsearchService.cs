using Nest;
using PermissionsApp.Application.Interfaces;
using PermissionsApp.Domain;

namespace PermissionsApp.Infrastructure
{

    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _client;

        public ElasticsearchService(IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
     .DefaultIndex("permissions");

            _client = new ElasticClient(settings);
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            await _client.IndexDocumentAsync(permission);
        }
    }
}
