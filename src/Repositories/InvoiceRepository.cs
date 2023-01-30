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


        public void Insert(Invoice invoice)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        INSERT INTO invoice
                            (description,  issue_date, subtotal_service, 
                            subtotal_mileage_allowance, total) 
                        VALUES
                            (@description, @issue_date, @subtotal_service, 
                            @subtotal_mileage_allowance, @total)",
                        connection);

                    command.Parameters.AddWithValue("@description", invoice.Description);
                    command.Parameters.AddWithValue("@issue_date", invoice.IssueDate);
                    command.Parameters.AddWithValue("@subtotal_service", invoice.SubtotalService);
                    command.Parameters.AddWithValue("@subtotal_mileage_allowance", invoice.SubtotalMileageAllowance);
                    command.Parameters.AddWithValue("@total", invoice.Total);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateTotals(Invoice invoice)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        UPDATE 
                            invoice
                        SET
                            subtotal_service = @subtotal_service, 
                            subtotal_mileage_allowance = @subtotal_mileage_allowance, 
                            total = @total
                        WHERE
                            id = @id",
                        connection);

                    command.Parameters.AddWithValue("@subtotal_service", invoice.SubtotalService);
                    command.Parameters.AddWithValue("@subtotal_mileage_allowance", invoice.SubtotalMileageAllowance);
                    command.Parameters.AddWithValue("@total", invoice.Total);
                    command.Parameters.AddWithValue("@id", invoice.Id);
                    command.ExecuteNonQuery();
                }
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
                    connection.Open();

                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT 
                                id 
                            FROM 
                                invoice 
                            WHERE 
                                id = @id";
                        command.Parameters.AddWithValue("@id", id);
                        
                        var reader = command.ExecuteReader();
                        if (!reader.HasRows)
                            return false;
                    }
                }

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            DELETE FROM 
                                invoice 
                            WHERE 
                                id = @id";
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
