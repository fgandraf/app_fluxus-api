using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;

namespace FluxusApi.Repositories.Contracts;

public interface IProfessionalRepository
{
    Task<IEnumerable> GetIndexAsync();
    Task<IEnumerable> GetTagNameidAsync();
    Task<long> InsertAsync(ProfessionalDTO model);
    Task<bool> UpdateAsync(ProfessionalDTO model);
    Task<bool> DeleteAsync(ProfessionalDTO model);
    Task<ProfessionalDTO> GetAsync(int id);
}