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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var invoices = connection.Query(@"
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
                        DESC");
                    return invoices;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public string GetDescription(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var invoice = connection.QueryFirst(@"
                        SELECT 
                            Description 
                        FROM 
                            Invoice 
                        WHERE 
                            Id = @id", new { id });
                    return invoice.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Insert(Invoice invoice)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO Invoice
                        (Description,  IssueDate, SubtotalService, 
                        SubtotalMileageAllowance, Total) 
                    VALUES
                        (@Description, @Issue_date, @SubtotalService, 
                        @SubtotalMileageAllowance, @Total)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(insertSQL, invoice);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int UpdateTotals(Invoice invoice)
        {
            try
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

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(updateSQL, invoice);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Delete(int id)
        {
            try
            {
                string deleteSQL = @"
                    DELETE FROM 
                        Invoice 
                    WHERE 
                        Id = @Id";

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
