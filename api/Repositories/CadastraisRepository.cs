using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{


    public class CadastraisRepository
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
                    Cadastrais cadastrais = new Cadastrais();
                    if (dr.Read())
                    {
                        cadastrais.Cnpj = Convert.ToString(dr["cnpj"]);
                        cadastrais.Nome = Convert.ToString(dr["fantasia"]);
                        cadastrais.RazaoSocial = Convert.ToString(dr["razao"]);
                        cadastrais.InscricaoEstadual = Convert.ToString(dr["ie"]);
                        cadastrais.InscricaoMunicipal = Convert.ToString(dr["im"]);
                        cadastrais.Endereco = Convert.ToString(dr["endereco"]);
                        cadastrais.Complemento = Convert.ToString(dr["complemento"]);
                        cadastrais.Bairro = Convert.ToString(dr["bairro"]);
                        cadastrais.Cidade = Convert.ToString(dr["cidade"]);
                        cadastrais.Cep = Convert.ToString(dr["cep"]);
                        cadastrais.Uf = Convert.ToString(dr["uf"]);
                        cadastrais.Constituicao = Convert.ToString(dr["constituicao"]);
                        cadastrais.Telefone = Convert.ToString(dr["telefone"]);
                        cadastrais.Telefone2 = Convert.ToString(dr["telefone2"]);
                        cadastrais.Email = Convert.ToString(dr["email"]);
                        cadastrais.BancoNome = Convert.ToString(dr["db_banco"]);
                        cadastrais.BancoTipo = Convert.ToString(dr["db_tipo"]);
                        cadastrais.BancoAgencia = Convert.ToString(dr["db_agencia"]);
                        cadastrais.BancoOperador = Convert.ToString(dr["db_operador"]);
                        cadastrais.BancoConta = Convert.ToString(dr["db_conta"]);
                        cadastrais.ContratoTomador = Convert.ToString(dr["ct_tomador"]);
                        cadastrais.ContratoEdital = Convert.ToString(dr["ct_edital"]);
                        cadastrais.ContratoNumero = Convert.ToString(dr["ct_contrato"]);
                        cadastrais.ContratoCelebrado = Convert.ToString(dr["ct_celebrado"]);
                        cadastrais.ContratoInicio = Convert.ToString(dr["ct_inicio"]);
                        cadastrais.ContratoTermino = Convert.ToString(dr["ct_termino"]);
                        cadastrais.Logotipo = Convert.ToBase64String((byte[])(dr["logo"]));

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





        public long Insert(Cadastrais dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_dadoscadastrais(id, cnpj, fantasia, razao, ie, im, endereco, complemento, bairro, cidade, cep, uf, constituicao, telefone, telefone2, email, db_banco, db_tipo, db_agencia, db_operador, db_conta, ct_tomador, ct_edital, ct_contrato, ct_celebrado, ct_inicio, ct_termino, logo) VALUES (@id, @cnpj, @fantasia, @razao, @ie, @im, @endereco, @complemento, @bairro, @cidade, @cep, @uf, @constituicao, @telefone, @telefone2, @email, @db_banco, @db_tipo, @db_agencia, @db_operador, @db_conta, @ct_tomador, @ct_edital, @ct_contrato, @ct_celebrado, @ct_inicio, @ct_termino, @logo)", conexao);
                sql.Parameters.AddWithValue("@id", "1");
                sql.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                sql.Parameters.AddWithValue("@fantasia", dado.Nome);
                sql.Parameters.AddWithValue("@razao", dado.RazaoSocial);
                sql.Parameters.AddWithValue("@ie", dado.InscricaoEstadual);
                sql.Parameters.AddWithValue("@im", dado.InscricaoMunicipal);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);
                sql.Parameters.AddWithValue("@complemento", dado.Complemento);
                sql.Parameters.AddWithValue("@bairro", dado.Bairro);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@cep", dado.Cep);
                sql.Parameters.AddWithValue("@uf", dado.Uf);
                sql.Parameters.AddWithValue("@constituicao", Util.DateOrNull(dado.Constituicao));
                sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                sql.Parameters.AddWithValue("@telefone2", dado.Telefone);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@db_banco", dado.BancoNome);
                sql.Parameters.AddWithValue("@db_tipo", dado.BancoTipo);
                sql.Parameters.AddWithValue("@db_agencia", dado.BancoAgencia);
                sql.Parameters.AddWithValue("@db_operador", dado.BancoOperador);
                sql.Parameters.AddWithValue("@db_conta", dado.BancoConta);
                sql.Parameters.AddWithValue("@ct_tomador", dado.ContratoTomador);
                sql.Parameters.AddWithValue("@ct_edital", dado.ContratoEdital);
                sql.Parameters.AddWithValue("@ct_contrato", dado.ContratoNumero);
                sql.Parameters.AddWithValue("@ct_celebrado", Util.DateOrNull(dado.ContratoCelebrado));
                sql.Parameters.AddWithValue("@ct_inicio", Util.DateOrNull(dado.ContratoInicio));
                sql.Parameters.AddWithValue("@ct_termino", Util.DateOrNull(dado.ContratoTermino));
                sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logotipo));

                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
            
        }





        public void Update(Cadastrais dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();

                MySqlCommand sql = new MySqlCommand("UPDATE tb_dadoscadastrais SET cnpj = @cnpj, fantasia = @fantasia, razao = @razao, ie = @ie, im = @im, endereco = @endereco, complemento = @complemento, bairro = @bairro, cidade = @cidade, cep = @cep, uf = @uf, constituicao = @constituicao, telefone = @telefone, telefone2 = @telefone2, email = @email, db_banco = @db_banco, db_tipo = @db_tipo, db_agencia = @db_agencia, db_operador = @db_operador, db_conta = @db_conta, ct_tomador = @ct_tomador, ct_edital = @ct_edital, ct_contrato = @ct_contrato, ct_celebrado = @ct_celebrado, ct_inicio = @ct_inicio, ct_termino = @ct_termino, logo = @logo WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", "1");
                sql.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                sql.Parameters.AddWithValue("@fantasia", dado.Nome);
                sql.Parameters.AddWithValue("@razao", dado.RazaoSocial);
                sql.Parameters.AddWithValue("@ie", dado.InscricaoEstadual);
                sql.Parameters.AddWithValue("@im", dado.InscricaoMunicipal);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);
                sql.Parameters.AddWithValue("@complemento", dado.Complemento);
                sql.Parameters.AddWithValue("@bairro", dado.Bairro);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@cep", dado.Cep);
                sql.Parameters.AddWithValue("@uf", dado.Uf);
                sql.Parameters.AddWithValue("@constituicao", Util.DateOrNull(dado.Constituicao));
                sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                sql.Parameters.AddWithValue("@telefone2", dado.Telefone2);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@db_banco", dado.BancoNome);
                sql.Parameters.AddWithValue("@db_tipo", dado.BancoTipo);
                sql.Parameters.AddWithValue("@db_agencia", dado.BancoAgencia);
                sql.Parameters.AddWithValue("@db_operador", dado.BancoOperador);
                sql.Parameters.AddWithValue("@db_conta", dado.BancoConta);
                sql.Parameters.AddWithValue("@ct_tomador", dado.ContratoTomador);
                sql.Parameters.AddWithValue("@ct_edital", dado.ContratoEdital);
                sql.Parameters.AddWithValue("@ct_contrato", dado.ContratoNumero);
                sql.Parameters.AddWithValue("@ct_celebrado", Util.DateOrNull(dado.ContratoCelebrado));
                sql.Parameters.AddWithValue("@ct_inicio", Util.DateOrNull(dado.ContratoInicio));
                sql.Parameters.AddWithValue("@ct_termino", Util.DateOrNull(dado.ContratoTermino));
                sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logotipo));

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
