using System;
using System.Collections;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;

namespace FluxusApi.Repositories
{
    public class ServiceRepository
    {
        public ArrayList GetAll()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT id, codigo, descricao, valor_atividade, valor_deslocamento FROM tb_atividades ORDER BY codigo", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                ArrayList atividadesArray = new ArrayList();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Service atividade = new Service();

                        atividade.Id = Convert.ToInt64(dr["id"]);
                        atividade.Tag = Convert.ToString(dr["codigo"]);
                        atividade.Description = Convert.ToString(dr["descricao"]);
                        atividade.ServiceAmount = Convert.ToString(dr["valor_atividade"]);
                        atividade.MileageAllowance = Convert.ToString(dr["valor_deslocamento"]);

                        atividadesArray.Add(atividade);
                    }
                    conexao.Close();
                    return atividadesArray;
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





        public Service GetBy(long id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_atividades WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    Service atividade = new Service();
                    if (dr.Read())
                    {
                        atividade.Id = Convert.ToInt64(dr["id"]);
                        atividade.Tag = Convert.ToString(dr["codigo"]);
                        atividade.Description = Convert.ToString(dr["descricao"]);
                        atividade.ServiceAmount = Convert.ToString(dr["valor_atividade"]);
                        atividade.MileageAllowance = Convert.ToString(dr["valor_deslocamento"]);
                    }
                    conexao.Close();
                    return atividade;
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





        public long Insert(Service dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_atividades(codigo, descricao, valor_atividade, valor_deslocamento) VALUES (@codigo, @descricao, @valor_atividade, @valor_deslocamento)", conexao);
                sql.Parameters.AddWithValue("@codigo", dado.Tag);
                sql.Parameters.AddWithValue("@descricao", dado.Description);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ServiceAmount);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.MileageAllowance);
                sql.ExecuteNonQuery();

                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }





        public void Update(long id, Service dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_atividades SET descricao = @descricao, valor_atividade = @valor_atividade, valor_deslocamento = @valor_deslocamento WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@descricao", dado.Description);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ServiceAmount);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.MileageAllowance);
                sql.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }





        public void Delete(long id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("DELETE FROM tb_atividades WHERE id = @id", conexao);
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