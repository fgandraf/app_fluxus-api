using System.Collections;
using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IServiceRepository
{
    Task<long> InsertAsync(Service model);
    Task<bool> UpdateAsync(Service model);
    Task<bool> DeleteAsync(Service model);
    Task<Service> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}