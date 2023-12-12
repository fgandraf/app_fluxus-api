using System.Collections;
using FluxusApi.Models;
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
    Task<long> InsertAsync(ServiceOrder model);
    Task<bool> UpdateAsync(ServiceOrder model);
    Task<bool> DeleteAsync(ServiceOrder model);
    Task<ServiceOrder> GetAsync(int id);
    Task<IEnumerable> GetAllAsync();
}