using System.Collections;
using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IInvoiceRepository
{
    Task<string> GetDescriptionAsync(int id);
    Task<int> UpdateTotalsAsync(Invoice invoice);
    Task<long> InsertAsync(Invoice model);
    Task<bool> UpdateAsync(Invoice model);
    Task<bool> DeleteAsync(Invoice model);
    Task<Invoice> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}