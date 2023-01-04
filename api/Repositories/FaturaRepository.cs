using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{


    class FaturaRepository
    {
        public ArrayList GetAll()
        {
            try
            {
                ArrayList faturaArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_fatura ORDER BY data DESC", conexao);
                MySqlDataReader dr = sql.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Fatura fatura = new Fatura();

                        fatura.Id = Convert.ToInt32(dr["id"]);
                        fatura.Descricao = Convert.ToString(dr["descricao"]);
                        fatura.Data = Convert.ToDateTime(dr["data"]);
                        fatura.SubtotalOs = Convert.ToDouble(dr["subtotal_os"]);
                        fatura.SubtotalDeslocamento = Convert.ToDouble(dr["subtotal_desloc"]);
                        fatura.Total = Convert.ToDouble(dr["total"]);

                        faturaArray.Add(fatura);
                    }
                    conexao.Close();
                    return faturaArray;
                }
                else
                {
                    conexao.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetDescricaoBy(string id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT descricao FROM tb_fatura WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = sql.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    conexao.Close();
                    return Convert.ToString(dr["descricao"]);
                }
                else
                {
                    conexao.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long Insert(Fatura dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_fatura(descricao, data, subtotal_os, subtotal_desloc, total) VALUES (@descricao, @data, @subtotal_os, @subtotal_desloc, @total)", conexao);
                sql.Parameters.AddWithValue("@descricao", dado.Descricao);
                sql.Parameters.AddWithValue("@data", dado.Data);
                sql.Parameters.AddWithValue("@subtotal_os", dado.SubtotalOs);
                sql.Parameters.AddWithValue("@subtotal_desloc", dado.SubtotalDeslocamento);
                sql.Parameters.AddWithValue("@total", dado.Total);
                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateTotals(long id, Fatura dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();

                MySqlCommand sql = new MySqlCommand("UPDATE tb_fatura SET subtotal_os = @subtotal_os, subtotal_desloc = @subtotal_desloc, total = @total WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@subtotal_os", dado.SubtotalOs);
                sql.Parameters.AddWithValue("@subtotal_desloc", dado.SubtotalDeslocamento);
                sql.Parameters.AddWithValue("@total", dado.Total);
                sql.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();

                MySqlCommand sql = new MySqlCommand("DELETE FROM tb_fatura WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                sql.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
