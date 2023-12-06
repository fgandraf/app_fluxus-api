using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using Dapper;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public InvoiceRepository(MySqlConnection connection) : base(connection) { }
        
        public async Task<string> GetDescriptionAsync(int id)
        {
            string query = @"SELECT Description FROM Invoice WHERE Id = @id";

            return await Connection.QueryFirstAsync(query, new { id = id });
        }
        
        public async Task<int> UpdateTotalsAsync(Invoice invoice)
        {
            string updateSQL = @"
                UPDATE 
                    Invoice
                SET
                    SubtotalService = @SubtotalService, 
                    SubtotalMileageAllowance = @SubtotalMileageAllowance, 
                    Total = @Total
                WHERE
                    Id = @Id";

            return await Connection.ExecuteAsync(updateSQL, invoice);
        }
    }
}
