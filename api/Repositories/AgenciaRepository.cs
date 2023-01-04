using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{
    public class AgenciaRepository
    {
        public ArrayList GetAll()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT id, agencia, nome, cidade, telefone1, email FROM tb_agencias ORDER BY agencia", conexao);

                MySqlDataReader dr = sql.ExecuteReader();

                ArrayList agenciaArray = new ArrayList();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic agencia = new
                        {
                            Id = Convert.ToInt64(dr["id"]),
                            Numero = Convert.ToString(dr["agencia"]),
                            Nome = Convert.ToString(dr["nome"]),
                            Cidade = Convert.ToString(dr["cidade"]),
                            Telefone1 = Convert.ToString(dr["telefone1"]),
                            Email = Convert.ToString(dr["email"])
                        };
                        agenciaArray.Add(agencia);
                    }

                    conexao.Close();
                    return agenciaArray;

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

        public Agencia GetBy(long id)
        {
            
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_agencias WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    Agencia agencia = new Agencia();

                    if (dr.Read())
                    {
                        agencia.Id = Convert.ToInt64(dr["id"]);
                        agencia.Numero = Convert.ToString(dr["agencia"]);
                        agencia.Nome = Convert.ToString(dr["nome"]);
                        agencia.Endereco = Convert.ToString(dr["endereco"]);
                        agencia.Complemento = Convert.ToString(dr["complemento"]);
                        agencia.Bairro = Convert.ToString(dr["bairro"]);
                        agencia.Cidade = Convert.ToString(dr["cidade"]);
                        agencia.CEP = Convert.ToString(dr["cep"]);
                        agencia.UF = Convert.ToString(dr["uf"]);
                        agencia.Telefone1 = Convert.ToString(dr["telefone1"]);
                        agencia.Telefone2 = Convert.ToString(dr["telefone2"]);
                        agencia.Email = Convert.ToString(dr["email"]);
                    }
                    conexao.Close();
                    return agencia;
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

        public ArrayList GetNomeTelefone1EmailBy(string agenciaCodigo)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT agencia, nome, telefone1, email FROM tb_agencias WHERE agencia = @agencia", conexao);
                sql.Parameters.AddWithValue("@agencia", agenciaCodigo);
                MySqlDataReader dr = sql.ExecuteReader();

                ArrayList agenciaArray = new ArrayList();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        dynamic agencia = new
                        {
                            Agencia = Convert.ToString(dr["agencia"]),
                            Nome = Convert.ToString(dr["nome"]),
                            Telefone1 = Convert.ToString(dr["telefone1"]),
                            Email = Convert.ToString(dr["email"])
                        };

                        agenciaArray.Add(agencia);
                    }
                    conexao.Close();
                    return agenciaArray;
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

        public long Insert(Agencia dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO  tb_agencias(agencia, nome, endereco, complemento, bairro, cidade, CEP, UF, contato, telefone1, telefone2, email) VALUES (@agencia, @nome, @endereco, @complemento, @bairro, @cidade, @CEP, @UF, @contato, @telefone1, @telefone2, @email)", conexao);

                sql.Parameters.AddWithValue("@agencia", dado.Numero);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);
                sql.Parameters.AddWithValue("@complemento", dado.Complemento);
                sql.Parameters.AddWithValue("@bairro", dado.Bairro);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@CEP", dado.CEP);
                sql.Parameters.AddWithValue("@UF", dado.UF);
                sql.Parameters.AddWithValue("@contato", dado.Contato);
                sql.Parameters.AddWithValue("@telefone1", dado.Telefone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Telefone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.ExecuteNonQuery();

                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(long id, Agencia dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_agencias SET agencia = @agencia, nome = @nome, endereco = @endereco, complemento = @complemento, bairro = @bairro, cidade = @cidade, CEP = @CEP, UF = @UF, contato = @contato, telefone1 = @telefone1, telefone2 = @telefone2, email = @email WHERE id = @id", conexao);

                sql.Parameters.AddWithValue("@agencia", dado.Numero);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);
                sql.Parameters.AddWithValue("@complemento", dado.Complemento);
                sql.Parameters.AddWithValue("@bairro", dado.Bairro);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@CEP", dado.CEP);
                sql.Parameters.AddWithValue("@UF", dado.UF);
                sql.Parameters.AddWithValue("@contato", dado.Contato);
                sql.Parameters.AddWithValue("@telefone1", dado.Telefone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Telefone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@id", id);
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
                MySqlCommand sql = new MySqlCommand("DELETE FROM tb_agencias WHERE id = @id", conexao);
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
