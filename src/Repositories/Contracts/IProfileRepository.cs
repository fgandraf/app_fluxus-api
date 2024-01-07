using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;
using FluxusApi.Models.ViewModels;

namespace FluxusApi.Repositories.Contracts;

public interface IProfileRepository
{
    Task<ProfileToPrintViewModel> GetToPrintAsync();
    Task<string> GetTradingNameAsync();
    Task<long> InsertAsync(ProfileDTO model);
    Task<bool> UpdateAsync(ProfileDTO model);
    Task<bool> DeleteAsync(ProfileDTO model);
    Task<ProfileDTO> GetAsync(int id);
    Task<List<ProfileDTO>> GetAllAsync();
}