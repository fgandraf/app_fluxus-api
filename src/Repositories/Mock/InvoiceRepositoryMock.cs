using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class InvoiceRepositoryMock : RepositoryMock<InvoiceDTO>, IInvoiceRepository
{

    public InvoiceRepositoryMock()
        => InitializeData();
    
    
    public Task<string> GetDescriptionAsync(int id)
    {
        var description = Repository
            .Where(x => x.Id == id)
            .Select(x => x.Description)
            .SingleOrDefault();

        return Task.FromResult(description);
    }

    public Task<int> UpdateTotalsAsync(InvoiceDTO invoiceDto)
    {
        var invoice = Repository.SingleOrDefault(x => x.Id == invoiceDto.Id);
        
        if (invoice == null)
            throw new KeyNotFoundException();

        invoice.SubtotalService = invoiceDto.SubtotalService;
        invoice.SubtotalMileageAllowance = invoiceDto.SubtotalMileageAllowance;
        invoice.Total = invoiceDto.Total;
        
        return Task.FromResult(1);
    }
    
    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<InvoiceDTO>
            {
                new InvoiceDTO { Id = 1, Description = "JUNHO-2023", IssueDate = DateTime.Parse("2023-06-30T00:00:00"), SubtotalService = 4428.00m, SubtotalMileageAllowance = 444.00m, Total = 4872.00m },
                new InvoiceDTO { Id = 2, Description = "JULHO-2023", IssueDate = DateTime.Parse("2023-07-31T00:00:00"), SubtotalService = 6273.00m, SubtotalMileageAllowance = 629.00m, Total = 6902.00m },
                new InvoiceDTO { Id = 3, Description = "AGOSTO-2023", IssueDate = DateTime.Parse("2023-08-31T00:00:00"), SubtotalService = 5166.00m, SubtotalMileageAllowance = 518.00m, Total = 5684.00m }
            };
        }
    }



}