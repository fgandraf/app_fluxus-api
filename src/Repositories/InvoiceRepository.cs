using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{


    public class InvoiceRepository
    {
        private string _connectionString = string.Empty;
        public InvoiceRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public ArrayList GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            invoice 
                        ORDER BY 
                            issue_date
                        DESC",
                        connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var invoices = new ArrayList();

                        while (reader.Read())
                        {
                            Invoice invoice = new Invoice();

                            invoice.Id = Convert.ToInt32(reader["id"]);
                            invoice.Description = Convert.ToString(reader["description"]);
                            invoice.IssueDate = Convert.ToDateTime(reader["issue_date"]);
                            invoice.SubtotalService = Convert.ToDouble(reader["subtotal_service"]);
                            invoice.SubtotalMileageAllowance = Convert.ToDouble(reader["subtotal_mileage_allowance"]);
                            invoice.Total = Convert.ToDouble(reader["total"]);

                            invoices.Add(invoice);
                        }

                        return invoices;
                    }
                    else
                        return null;
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
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            description 
                        FROM 
                            invoice 
                        WHERE 
                            id = @id",
                        connection);
                    command.Parameters.AddWithValue("@id", id);
                    
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return Convert.ToString(reader["description"]);
                    }
                    else
                        return null;
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
