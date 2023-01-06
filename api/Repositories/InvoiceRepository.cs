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

                    var sql = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            invoice 
                        ORDER BY 
                            issue_date
                        DESC",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();


                    if (dr.HasRows)
                    {
                        var invoices = new ArrayList();

                        while (dr.Read())
                        {
                            Invoice invoice = new Invoice();

                            invoice.Id = Convert.ToInt32(dr["id"]);
                            invoice.Description = Convert.ToString(dr["description"]);
                            invoice.IssueDate = Convert.ToDateTime(dr["issue_date"]);
                            invoice.SubtotalService = Convert.ToDouble(dr["subtotal_service"]);
                            invoice.SubtotalMileageAllowance = Convert.ToDouble(dr["subtotal_mileage_allowance"]);
                            invoice.Total = Convert.ToDouble(dr["total"]);

                            invoices.Add(invoice);
                        }

                        return invoices;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public string GetDescription(string id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        SELECT 
                            description 
                        FROM 
                            invoice 
                        WHERE 
                            id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        return Convert.ToString(dr["description"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public void Insert(Invoice dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        INSERT INTO invoice
                            (description,  issue_date, subtotal_service, 
                            subtotal_mileage_allowance, total) 
                        VALUES
                            (@description, @issue_date, @subtotal_service, 
                            @subtotal_mileage_allowance, @total)",
                        connection);

                    sql.Parameters.AddWithValue("@description", dado.Description);
                    sql.Parameters.AddWithValue("@issue_date", dado.IssueDate);
                    sql.Parameters.AddWithValue("@subtotal_service", dado.SubtotalService);
                    sql.Parameters.AddWithValue("@subtotal_mileage_allowance", dado.SubtotalMileageAllowance);
                    sql.Parameters.AddWithValue("@total", dado.Total);
                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateTotals(long id, Invoice dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        UPDATE 
                            invoice
                        SET
                            subtotal_service = @subtotal_service, 
                            subtotal_mileage_allowance = @subtotal_mileage_allowance, 
                            total = @total
                        WHERE
                            id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@subtotal_service", dado.SubtotalService);
                    sql.Parameters.AddWithValue("@subtotal_mileage_allowance", dado.SubtotalMileageAllowance);
                    sql.Parameters.AddWithValue("@total", dado.Total);
                    sql.Parameters.AddWithValue("@id", id);
                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public bool Delete(string id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sqlSelect = new MySqlCommand(@"
                        SELECT 
                            id 
                        FROM 
                            invoice 
                        WHERE 
                            id = @id",
                        connection);

                    sqlSelect.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = sqlSelect.ExecuteReader();

                    if (!dr.HasRows)
                        return false;
                }

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        DELETE FROM 
                            invoice 
                        WHERE 
                            id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);
                    sql.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
