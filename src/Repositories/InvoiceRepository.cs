using MySql.Data.MySqlClient;
using FluxusApi.Models;
using Dapper;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories;

public class InvoiceRepository : Repository<InvoiceDTO>, IInvoiceRepository
{
    public InvoiceRepository(MySqlConnection connection) : base(connection) { }
        
    public async Task<string> GetDescriptionAsync(int id)
    {
        const string query = @"SELECT Description FROM Invoice WHERE Id = @id";

        return await Connection.QueryFirstAsync(query, new { id = id });
    }
        
    public async Task<int> UpdateTotalsAsync(InvoiceDTO invoiceDto)
    {
        const string query = @"
                UPDATE 
                    Invoice
                SET
                    SubtotalService = @SubtotalService, 
                    SubtotalMileageAllowance = @SubtotalMileageAllowance, 
                    Total = @Total
                WHERE
                    Id = @Id";

        return await Connection.ExecuteAsync(query, invoiceDto);
    }
}