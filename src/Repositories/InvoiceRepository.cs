using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public string GetDescription(int id)
        {
            string query = @"SELECT Description FROM Invoice WHERE Id = @id";

            try
            {
                return Connection.QueryFirst<string>(query, new { id = id });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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

            try
            {
                return Connection.Execute(updateSQL, invoice);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
