using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(MySqlConnection connection) : base(connection) { }
}