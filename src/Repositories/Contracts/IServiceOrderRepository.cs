using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;
using FluxusApi.Models.Enums;

namespace FluxusApi.Repositories.Contracts;

public interface IServiceOrderRepository
{
    Task<IEnumerable> GetOrdersFlowAsync();
    Task<IEnumerable> GetInvoicedAsync(int invoiceId);
    Task<IEnumerable> GetDoneToInvoiceAsync();
    Task<IEnumerable> GetFilteredAsync(string filter);
    Task<IEnumerable> GetProfessionalAsync(int invoiceId);
    Task<IEnumerable> GetOrderedCitiesAsync();
    Task<int> UpdateInvoiceIdAsync(int invoiceId, List<int> orders);
    Task<int> UpdateStatusAsync(int id, EnumStatus status);
    Task<long> InsertAsync(ServiceOrderDTO model);
    Task<bool> UpdateAsync(ServiceOrderDTO model);
    Task<bool> DeleteAsync(ServiceOrderDTO model);
    Task<ServiceOrderDTO> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}