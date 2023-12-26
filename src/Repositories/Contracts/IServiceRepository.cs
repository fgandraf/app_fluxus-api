using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;

namespace FluxusApi.Repositories.Contracts;

public interface IServiceRepository
{
    Task<long> InsertAsync(ServiceDTO model);
    Task<bool> UpdateAsync(ServiceDTO model);
    Task<bool> DeleteAsync(ServiceDTO model);
    Task<ServiceDTO> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}