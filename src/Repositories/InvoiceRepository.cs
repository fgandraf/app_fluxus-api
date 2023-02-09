using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class InvoiceRepository
    {
        private string _connectionString = string.Empty;

        public InvoiceRepository()
            => _connectionString = ConnectionString.Get();


        public IEnumerable GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.GetAll<Invoice>(); //Order By IssueDate DESC
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


        public void Insert(Invoice invoice)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Insert<Invoice>(invoice);
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


        public bool Delete(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var invoice = connection.Get<Invoice>(id);
                    return connection.Delete<Invoice>(invoice);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
