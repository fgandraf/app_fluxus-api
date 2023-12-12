using System.Collections;
using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IBankBranchRepository
{
    Task<IEnumerable> GetIndexAsync();
    Task<IEnumerable> GetContactsAsync(string id);
    Task<long> InsertAsync(BankBranch model);
    Task<bool> UpdateAsync(BankBranch model);
    Task<bool> DeleteAsync(BankBranch model);
    Task<BankBranch> GetAsync(string id);
    Task<IEnumerable> GetAllAsync();
}