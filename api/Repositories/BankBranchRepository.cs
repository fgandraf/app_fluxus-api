using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{
    public class BankBranchRepository
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

        public BankBranch GetBy(long id)
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
                    BankBranch agencia = new BankBranch();

                    if (dr.Read())
                    {
                        agencia.Id = Convert.ToInt64(dr["id"]);
                        agencia.BranchNumber = Convert.ToString(dr["agencia"]);
                        agencia.Name = Convert.ToString(dr["nome"]);
                        agencia.Address = Convert.ToString(dr["endereco"]);
                        agencia.Complement = Convert.ToString(dr["complemento"]);
                        agencia.District = Convert.ToString(dr["bairro"]);
                        agencia.City = Convert.ToString(dr["cidade"]);
                        agencia.Zip = Convert.ToString(dr["cep"]);
                        agencia.State = Convert.ToString(dr["uf"]);
                        agencia.Phone1 = Convert.ToString(dr["telefone1"]);
                        agencia.Phone2 = Convert.ToString(dr["telefone2"]);
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

        public long Insert(BankBranch dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO  tb_agencias(agencia, nome, endereco, complemento, bairro, cidade, CEP, UF, contato, telefone1, telefone2, email) VALUES (@agencia, @nome, @endereco, @complemento, @bairro, @cidade, @CEP, @UF, @contato, @telefone1, @telefone2, @email)", conexao);

                sql.Parameters.AddWithValue("@agencia", dado.BranchNumber);
                sql.Parameters.AddWithValue("@nome", dado.Name);
                sql.Parameters.AddWithValue("@endereco", dado.Address);
                sql.Parameters.AddWithValue("@complemento", dado.Complement);
                sql.Parameters.AddWithValue("@bairro", dado.District);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@CEP", dado.Zip);
                sql.Parameters.AddWithValue("@UF", dado.State);
                sql.Parameters.AddWithValue("@contato", dado.ContactName);
                sql.Parameters.AddWithValue("@telefone1", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone2);
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

        public void Update(long id, BankBranch dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_agencias SET agencia = @agencia, nome = @nome, endereco = @endereco, complemento = @complemento, bairro = @bairro, cidade = @cidade, CEP = @CEP, UF = @UF, contato = @contato, telefone1 = @telefone1, telefone2 = @telefone2, email = @email WHERE id = @id", conexao);

                sql.Parameters.AddWithValue("@agencia", dado.BranchNumber);
                sql.Parameters.AddWithValue("@nome", dado.Name);
                sql.Parameters.AddWithValue("@endereco", dado.Address);
                sql.Parameters.AddWithValue("@complemento", dado.Complement);
                sql.Parameters.AddWithValue("@bairro", dado.District);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@CEP", dado.Zip);
                sql.Parameters.AddWithValue("@UF", dado.State);
                sql.Parameters.AddWithValue("@contato", dado.ContactName);
                sql.Parameters.AddWithValue("@telefone1", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone2);
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
