using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository
    {
        private string _connectionString = string.Empty;

        public InvoiceRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public IEnumerable GetAll()
        {
            string query = @"
                SELECT
                    Id,
                    Description,
                    IssueDate,
                    SubtotalService, 
                    SubtotalMileageAllowance,
                    Total 
                FROM 
                    Invoice 
                ORDER BY 
                    IssueDate
                DESC";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public string GetDescription(int id)
        {
            string query = @"
                SELECT 
                    Description 
                FROM 
                    Invoice 
                WHERE 
                    Id = @id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.QueryFirst(query, new { id });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Insert(Invoice invoice)
        {
            string insertSQL = @"
                INSERT INTO Invoice
                    (Description,  IssueDate, SubtotalService, 
                    SubtotalMileageAllowance, Total) 
                VALUES
                    (@Description, @Issue_date, @SubtotalService, 
                    @SubtotalMileageAllowance, @Total)";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(insertSQL, invoice);
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
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, invoice);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Delete(int id)
        {
            string deleteSQL = @"
                DELETE FROM 
                    Invoice 
                WHERE 
                    Id = @Id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(deleteSQL, new { id });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
