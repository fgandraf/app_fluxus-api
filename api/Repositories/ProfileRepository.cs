using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{


    public class ProfileRepository
    {
        public ArrayList GetAll()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_dadoscadastrais", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                ArrayList cadastraisArray = new ArrayList();

                if (dr.HasRows)
                {
                    Profile cadastrais = new Profile();
                    if (dr.Read())
                    {
                        cadastrais.Cnpj = Convert.ToString(dr["cnpj"]);
                        cadastrais.TradingName = Convert.ToString(dr["fantasia"]);
                        cadastrais.CompanyName = Convert.ToString(dr["razao"]);
                        cadastrais.StateId = Convert.ToString(dr["ie"]);
                        cadastrais.CityId = Convert.ToString(dr["im"]);
                        cadastrais.Address = Convert.ToString(dr["endereco"]);
                        cadastrais.Complement = Convert.ToString(dr["complemento"]);
                        cadastrais.District = Convert.ToString(dr["bairro"]);
                        cadastrais.City = Convert.ToString(dr["cidade"]);
                        cadastrais.Zip = Convert.ToString(dr["cep"]);
                        cadastrais.State = Convert.ToString(dr["uf"]);
                        cadastrais.EstablishmentDate = Convert.ToString(dr["constituicao"]);
                        cadastrais.Phone1 = Convert.ToString(dr["telefone"]);
                        cadastrais.Phone2 = Convert.ToString(dr["telefone2"]);
                        cadastrais.Email = Convert.ToString(dr["email"]);
                        cadastrais.BankAccountName = Convert.ToString(dr["db_banco"]);
                        cadastrais.BankAccountType = Convert.ToString(dr["db_tipo"]);
                        cadastrais.BankAccountBranch = Convert.ToString(dr["db_agencia"]);
                        cadastrais.BankAccountDigit = Convert.ToString(dr["db_operador"]);
                        cadastrais.BankAccountNumber = Convert.ToString(dr["db_conta"]);
                        cadastrais.ContractorName = Convert.ToString(dr["ct_tomador"]);
                        cadastrais.ContractNotice = Convert.ToString(dr["ct_edital"]);
                        cadastrais.ContractNumber = Convert.ToString(dr["ct_contrato"]);
                        cadastrais.ContractEstablished = Convert.ToString(dr["ct_celebrado"]);
                        cadastrais.ContractStart = Convert.ToString(dr["ct_inicio"]);
                        cadastrais.ContractEnd = Convert.ToString(dr["ct_termino"]);
                        cadastrais.Logo = Convert.ToBase64String((byte[])(dr["logo"]));

                        cadastraisArray.Add(cadastrais);
                    }
                    conexao.Close();
                    return cadastraisArray;
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

        public byte[] GetLogo()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT logo FROM tb_dadoscadastrais WHERE id = 1", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    conexao.Close();
                    return (byte[])(dr["logo"]);
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



        public ArrayList GetToPrint()
        {
            try
            {
                ArrayList cadastraisArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT cnpj, razao, ct_edital, ct_contrato, logo FROM tb_dadoscadastrais WHERE id = 1", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        dynamic cadastrais = new
                        {
                            Cnpj = Convert.ToString(dr["cnpj"]),
                            Razao = Convert.ToString(dr["razao"]),
                            Ct_edital = Convert.ToString(dr["ct_edital"]),
                            Ct_contrato = Convert.ToString(dr["ct_contrato"]),
                            Logo = (byte[])(dr["logo"])
                        };

                        cadastraisArray.Add(cadastrais);
                    }
                    conexao.Close();
                    return cadastraisArray;
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





        public string GetFantasia()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT fantasia FROM tb_dadoscadastrais WHERE id = 1", conexao);
                MySqlDataReader dr = sql.ExecuteReader();


                if (dr.HasRows)
                {
                    string fantasia = null;

                    if (dr.Read())
                    {
                        fantasia = Convert.ToString(dr["fantasia"]);
                    }
                    conexao.Close();
                    return fantasia;
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





        public long Insert(Profile dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_dadoscadastrais(id, cnpj, fantasia, razao, ie, im, endereco, complemento, bairro, cidade, cep, uf, constituicao, telefone, telefone2, email, db_banco, db_tipo, db_agencia, db_operador, db_conta, ct_tomador, ct_edital, ct_contrato, ct_celebrado, ct_inicio, ct_termino, logo) VALUES (@id, @cnpj, @fantasia, @razao, @ie, @im, @endereco, @complemento, @bairro, @cidade, @cep, @uf, @constituicao, @telefone, @telefone2, @email, @db_banco, @db_tipo, @db_agencia, @db_operador, @db_conta, @ct_tomador, @ct_edital, @ct_contrato, @ct_celebrado, @ct_inicio, @ct_termino, @logo)", conexao);
                sql.Parameters.AddWithValue("@id", "1");
                sql.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                sql.Parameters.AddWithValue("@fantasia", dado.TradingName);
                sql.Parameters.AddWithValue("@razao", dado.CompanyName);
                sql.Parameters.AddWithValue("@ie", dado.StateId);
                sql.Parameters.AddWithValue("@im", dado.CityId);
                sql.Parameters.AddWithValue("@endereco", dado.Address);
                sql.Parameters.AddWithValue("@complemento", dado.Complement);
                sql.Parameters.AddWithValue("@bairro", dado.District);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@cep", dado.Zip);
                sql.Parameters.AddWithValue("@uf", dado.State);
                sql.Parameters.AddWithValue("@constituicao", Util.DateOrNull(dado.EstablishmentDate));
                sql.Parameters.AddWithValue("@telefone", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone1);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@db_banco", dado.BankAccountName);
                sql.Parameters.AddWithValue("@db_tipo", dado.BankAccountType);
                sql.Parameters.AddWithValue("@db_agencia", dado.BankAccountBranch);
                sql.Parameters.AddWithValue("@db_operador", dado.BankAccountDigit);
                sql.Parameters.AddWithValue("@db_conta", dado.BankAccountNumber);
                sql.Parameters.AddWithValue("@ct_tomador", dado.ContractorName);
                sql.Parameters.AddWithValue("@ct_edital", dado.ContractNotice);
                sql.Parameters.AddWithValue("@ct_contrato", dado.ContractNumber);
                sql.Parameters.AddWithValue("@ct_celebrado", Util.DateOrNull(dado.ContractEstablished));
                sql.Parameters.AddWithValue("@ct_inicio", Util.DateOrNull(dado.ContractStart));
                sql.Parameters.AddWithValue("@ct_termino", Util.DateOrNull(dado.ContractEnd));
                sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logo));

                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
            
        }





        public void Update(Profile dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();

                MySqlCommand sql = new MySqlCommand("UPDATE tb_dadoscadastrais SET cnpj = @cnpj, fantasia = @fantasia, razao = @razao, ie = @ie, im = @im, endereco = @endereco, complemento = @complemento, bairro = @bairro, cidade = @cidade, cep = @cep, uf = @uf, constituicao = @constituicao, telefone = @telefone, telefone2 = @telefone2, email = @email, db_banco = @db_banco, db_tipo = @db_tipo, db_agencia = @db_agencia, db_operador = @db_operador, db_conta = @db_conta, ct_tomador = @ct_tomador, ct_edital = @ct_edital, ct_contrato = @ct_contrato, ct_celebrado = @ct_celebrado, ct_inicio = @ct_inicio, ct_termino = @ct_termino, logo = @logo WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", "1");
                sql.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                sql.Parameters.AddWithValue("@fantasia", dado.TradingName);
                sql.Parameters.AddWithValue("@razao", dado.CompanyName);
                sql.Parameters.AddWithValue("@ie", dado.StateId);
                sql.Parameters.AddWithValue("@im", dado.CityId);
                sql.Parameters.AddWithValue("@endereco", dado.Address);
                sql.Parameters.AddWithValue("@complemento", dado.Complement);
                sql.Parameters.AddWithValue("@bairro", dado.District);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@cep", dado.Zip);
                sql.Parameters.AddWithValue("@uf", dado.State);
                sql.Parameters.AddWithValue("@constituicao", Util.DateOrNull(dado.EstablishmentDate));
                sql.Parameters.AddWithValue("@telefone", dado.Phone1);
                sql.Parameters.AddWithValue("@telefone2", dado.Phone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@db_banco", dado.BankAccountName);
                sql.Parameters.AddWithValue("@db_tipo", dado.BankAccountType);
                sql.Parameters.AddWithValue("@db_agencia", dado.BankAccountBranch);
                sql.Parameters.AddWithValue("@db_operador", dado.BankAccountDigit);
                sql.Parameters.AddWithValue("@db_conta", dado.BankAccountNumber);
                sql.Parameters.AddWithValue("@ct_tomador", dado.ContractorName);
                sql.Parameters.AddWithValue("@ct_edital", dado.ContractNotice);
                sql.Parameters.AddWithValue("@ct_contrato", dado.ContractNumber);
                sql.Parameters.AddWithValue("@ct_celebrado", Util.DateOrNull(dado.ContractEstablished));
                sql.Parameters.AddWithValue("@ct_inicio", Util.DateOrNull(dado.ContractStart));
                sql.Parameters.AddWithValue("@ct_termino", Util.DateOrNull(dado.ContractEnd));
                sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logo));

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
