using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using Dapper;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public InvoiceRepository(MySqlConnection connection) : base(connection) { }

        public string GetDescription(int id)
        {
            string query = @"SELECT Description FROM Invoice WHERE Id = @id";

            return Connection.QueryFirst<string>(query, new { id = id });
        }

        public int UpdateTotals(Invoice invoice)
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

            return Connection.Execute(updateSQL, invoice);
        }
    }
}
