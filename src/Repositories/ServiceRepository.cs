using FluxusApi.Entities;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories
{
    public class ServiceRepository : Repository<Service>
    {
        public ServiceRepository(MySqlConnection connection) : base(connection) { }
    }
}