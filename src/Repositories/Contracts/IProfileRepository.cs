using System.Collections;
using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IProfileRepository
{
    Task<byte[]> GetLogoAsync();
    Task<string> UpdateLogoAsync(byte[] logoByte);
    Task<IEnumerable> GetToPrintAsync();
    Task<string> GetTradingNameAsync();
    Task<long> InsertAsync(Profile model);
    Task<bool> UpdateAsync(Profile model);
    Task<bool> DeleteAsync(Profile model);
    Task<Profile> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}