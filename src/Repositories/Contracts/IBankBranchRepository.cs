using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;

namespace FluxusApi.Repositories.Contracts;

public interface IBankBranchRepository
{
    Task<IEnumerable> GetIndexAsync();
    Task<IEnumerable> GetContactsAsync(string id);
    Task<long> InsertAsync(BankBranchDTO model);
    Task<bool> UpdateAsync(BankBranchDTO model);
    Task<bool> DeleteAsync(BankBranchDTO model);
    Task<BankBranchDTO> GetAsync(string id);
    Task<IEnumerable> GetAllAsync();
}