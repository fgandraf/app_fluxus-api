using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{


    public class ProfessionalRepository
    {
        public ArrayList GetAll()
        {
            try
            {
                ArrayList profissionalArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT id, codigo, nome, profissao, telefone1, usr_ativo FROM tb_profissionais ORDER BY codigo", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Professional profissional = new Professional();

                        profissional.Id = Convert.ToInt64(dr["id"]);
                        profissional.Tag = Convert.ToString(dr["codigo"]);
                        profissional.Name = Convert.ToString(dr["nome"]);
                        profissional.Profession = Convert.ToString(dr["profissao"]);
                        profissional.Phone1 = Convert.ToString(dr["telefone1"]);
                        profissional.UserActive = Convert.ToBoolean(dr["usr_ativo"]);

                        profissionalArray.Add(profissional);
                    }
                    conexao.Close();
                    return profissionalArray;
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

        public ArrayList GetCodigoENomeid()
        {
            try
            {
                ArrayList profissionalArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT codigo, nomeid FROM tb_profissionais ORDER BY codigo", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic profissional = new
                        {
                            Codigo = Convert.ToString(dr["codigo"]),
                            Nomeid = Convert.ToString(dr["nomeid"])
                        };

                        profissionalArray.Add(profissional);
                    }
                    conexao.Close();
                    return profissionalArray;
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

        public ArrayList GetUserInfoBy(string nomeDeUsuario)
        {
            try
            {
                ArrayList userArray = new ArrayList();

                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT codigo, rt, rl, usr_nome, usr_senha, usr_ativo FROM tb_profissionais WHERE usr_nome = @usr_nome", conexao);
                sql.Parameters.AddWithValue("@usr_nome", nomeDeUsuario);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        dynamic user = new
                        {
                            Codigo = Convert.ToString(dr["codigo"]),
                            Rt = Convert.ToBoolean(dr["rt"]),
                            Rl = Convert.ToBoolean(dr["rl"]),
                            Usr_ativo = Convert.ToBoolean(dr["usr_ativo"]),
                            Usr_nome = Convert.ToString(dr["usr_nome"]),
                            Usr_senha = Convert.ToString(dr["usr_senha"])
                        };
                        userArray.Add(user);
                    }
                    conexao.Close();
                    return userArray;
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

        public Professional GetBy(long id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_profissionais WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    Professional profissional = new Professional();
                    if (dr.Read())
                    {
                        profissional.Id = Convert.ToInt64(dr["id"]);
                        profissional.Tag = Convert.ToString(dr["codigo"]);
                        profissional.Name = Convert.ToString(dr["nome"]);
                        profissional.NameId = Convert.ToString(dr["nomeid"]);
                        profissional.Cpf = Convert.ToString(dr["cpf"]);
                        profissional.Birthday = Convert.ToString(dr["nascimento"]);
                        profissional.Profession = Convert.ToString(dr["profissao"]);
                        profissional.PermitNumber = Convert.ToString(dr["carteira"]);
                        profissional.Association = Convert.ToString(dr["entidade"]);
                        profissional.Phone1 = Convert.ToString(dr["telefone1"]);
                        profissional.Phone2 = Convert.ToString(dr["telefone2"]);
                        profissional.Email = Convert.ToString(dr["email"]);
                        profissional.TechnicianResponsible = Convert.ToBoolean(dr["rt"]);
                        profissional.LegalResponsible = Convert.ToBoolean(dr["rl"]);
                        profissional.UserActive = Convert.ToBoolean(dr["usr_ativo"]);
                        profissional.UserName = Convert.ToString(dr["usr_nome"]);
                        profissional.UserPassword = Convert.ToString(dr["usr_senha"]);
                    }
                    conexao.Close();
                    return profissional;
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

        public long Insert(Professional dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_profissionais(codigo, nome, nomeid, cpf, nascimento, profissao, carteira, entidade, telefone1, telefone2, email, rt, rl, usr_ativo, usr_nome, usr_senha) VALUES (@codigo, @nome, @nomeid, @cpf, @nascimento, @profissao, @carteira, @entidade, @telefone1, @telefone2, @email, @rt, @rl, @usr_ativo, @usr_nome, @usr_senha)", conexao);
                sql.Parameters.AddWithValue("@codigo", dado.Tag);
                sql.Parameters.AddWithValue("@nome", dado.Name);
                sql.Parameters.AddWithValue("@nomeid", dado.NameId);
                sql.Parameters.AddWithValue("@cpf", dado.Cpf);
                sql.Parameters.AddWithValue("@nascimento", Util.DateOrNull(dado.Birthday));
                sql.Parameters.AddWithValue("@profissao", dado.Profession);
                sql.Parameters.AddWithValue("@carteira", dado.PermitNumber);
                sql.Parameters.AddWithValue("@entidade", dado.Association);
                sql.Parameters.AddWithValue("@telefone1", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@rt", dado.TechnicianResponsible);
                sql.Parameters.AddWithValue("@rl", dado.LegalResponsible);
                sql.Parameters.AddWithValue("@usr_ativo", dado.UserActive);
                sql.Parameters.AddWithValue("@usr_nome", dado.UserName);
                sql.Parameters.AddWithValue("@usr_senha", dado.UserPassword);

                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(long id, Professional dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_profissionais SET nome = @nome, nomeid = @nomeid, cpf = @cpf, nascimento = @nascimento, profissao = @profissao, carteira = @carteira, entidade = @entidade, telefone1 = @telefone1, telefone2 = @telefone2, email = @email, rt = @rt, rl = @rl, usr_ativo = @usr_ativo, usr_nome = @usr_nome, usr_senha = @usr_senha WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@nome", dado.Name);
                sql.Parameters.AddWithValue("@nomeid", dado.NameId);
                sql.Parameters.AddWithValue("@cpf", dado.Cpf);
                sql.Parameters.AddWithValue("@nascimento", Util.DateOrNull(dado.Birthday));
                sql.Parameters.AddWithValue("@profissao", dado.Profession);
                sql.Parameters.AddWithValue("@carteira", dado.PermitNumber);
                sql.Parameters.AddWithValue("@entidade", dado.Association);
                sql.Parameters.AddWithValue("@telefone1", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@rt", dado.TechnicianResponsible);
                sql.Parameters.AddWithValue("@rl", dado.LegalResponsible);
                sql.Parameters.AddWithValue("@usr_ativo", dado.UserActive);
                sql.Parameters.AddWithValue("@usr_nome", dado.UserName);
                sql.Parameters.AddWithValue("@usr_senha", dado.UserPassword);
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
                MySqlCommand sql = new MySqlCommand("DELETE FROM tb_profissionais WHERE id = @id", conexao);
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
