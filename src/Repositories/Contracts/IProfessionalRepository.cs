using System.Collections;
using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IProfessionalRepository
{
    Task<IEnumerable> GetIndexAsync();
    Task<IEnumerable> GetTagNameidAsync();
    Task<long> InsertAsync(Professional model);
    Task<bool> UpdateAsync(Professional model);
    Task<bool> DeleteAsync(Professional model);
    Task<Professional> GetAsync(int id);
}