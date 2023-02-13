using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public InvoiceRepository(MySqlConnection connection) : base(connection) { }

        public string GetDescription(int id)
        {
            string query = @"SELECT Description FROM Invoice WHERE Id = @id";

            return _connection.QueryFirst<string>(query, new { id = id });
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

            return _connection.Execute(updateSQL, invoice);
        }
    }
}
