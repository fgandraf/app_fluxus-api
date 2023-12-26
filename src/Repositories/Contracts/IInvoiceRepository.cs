using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;

namespace FluxusApi.Repositories.Contracts;

public interface IInvoiceRepository
{
    Task<string> GetDescriptionAsync(int id);
    Task<int> UpdateTotalsAsync(InvoiceDTO invoiceDto);
    Task<long> InsertAsync(InvoiceDTO model);
    Task<bool> UpdateAsync(InvoiceDTO model);
    Task<bool> DeleteAsync(InvoiceDTO model);
    Task<InvoiceDTO> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}