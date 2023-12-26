using FluxusApi.Models;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories;

public class ServiceRepository : Repository<ServiceDTO>, IServiceRepository
{
    public ServiceRepository(MySqlConnection connection) : base(connection) { }
}