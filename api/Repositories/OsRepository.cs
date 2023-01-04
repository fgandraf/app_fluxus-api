using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;

namespace FluxusApi.Repositories
{


    public class OsRepository
    {
        public ArrayList GetOrdensDoFluxo()
        {
            try
            {
                ArrayList osArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT id, referencia, titulo, status, profissional_cod FROM tb_os WHERE fatura_cod = 0 ORDER BY data_ordem", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic osFluxo = new
                        {
                            Id = Convert.ToInt64(dr["id"]),
                            Referencia = Convert.ToString(dr["referencia"]),
                            Titulo = Convert.ToString(dr["titulo"]),
                            Status = Convert.ToString(dr["status"]),
                            Profissional_cod = Convert.ToString(dr["profissional_cod"])
                        };

                        osArray.Add(osFluxo);
                    }
                    conexao.Close();
                    return osArray;
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

        public ArrayList GetOrdensFaturadasBy(long fatura_cod)
        {
            try
            {
                ArrayList osArray = new ArrayList();

                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                //MySqlCommand sql = new MySqlCommand("SELECT t1.id, t1.data_ordem, t1.referencia, t1.profissional_cod, t1.atividade_cod, t1.cidade, t1.nome_cliente, t1.data_vistoria, t1.data_concluida, t1.fatura_cod, t1.status, t2.valor_atividade, t2.valor_deslocamento FROM tb_os t1 INNER JOIN tb_atividades t2 on t1.atividade_cod = t2.codigo WHERE t1.fatura_cod = @fatura_cod ORDER BY t1.data_concluida", conexao);
                MySqlCommand sql = new MySqlCommand("SELECT id, data_ordem, referencia, profissional_cod, atividade_cod, cidade, nome_cliente, data_vistoria, data_concluida, fatura_cod, status, valor_atividade, valor_deslocamento FROM tb_os WHERE fatura_cod = @fatura_cod ORDER BY data_concluida", conexao);
                sql.Parameters.AddWithValue("@fatura_cod", fatura_cod);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        dynamic osFatura = new
                        {
                            Id = Convert.ToInt64(dr["id"]),
                            Data_ordem = Convert.ToDateTime(dr["data_ordem"]),
                            Referencia = Convert.ToString(dr["referencia"]),
                            Profissional_cod = Convert.ToString(dr["profissional_cod"]),
                            Atividade_cod = Convert.ToString(dr["atividade_cod"]),
                            Cidade = Convert.ToString(dr["cidade"]),
                            Nome_cliente = Convert.ToString(dr["nome_cliente"]),
                            Data_vistoria = Convert.ToDateTime(dr["data_vistoria"]),
                            Data_concluida = Convert.ToDateTime(dr["data_concluida"]),
                            Fatura_cod = Convert.ToInt64(dr["fatura_cod"]),
                            Status = Convert.ToString(dr["status"]),
                            Valor_atividade = Convert.ToDouble(dr["valor_atividade"]),
                            Valor_deslocamento = Convert.ToDouble(dr["valor_deslocamento"])
                        };

                        osArray.Add(osFatura);

                    }
                    conexao.Close();
                    return osArray;
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

        public ArrayList GetOrdensConcluidasAFaturar()
        {
            try
            {
                ArrayList osArray = new ArrayList();

                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                //MySqlCommand sql = new MySqlCommand("SELECT t1.id, t1.data_ordem, t1.referencia, t1.profissional_cod, t1.atividade_cod, t1.cidade, t1.nome_cliente, t1.data_vistoria, t1.data_concluida, t2.valor_atividade, t2.valor_deslocamento FROM tb_os t1 INNER JOIN tb_atividades t2 on t1.atividade_cod = t2.codigo WHERE t1.fatura_cod = 0 AND status = 'CONCLUÍDA' ORDER BY t1.data_concluida", conexao);
                MySqlCommand sql = new MySqlCommand("SELECT id, data_ordem, referencia, profissional_cod, atividade_cod, cidade, nome_cliente, data_vistoria, data_concluida, valor_atividade, valor_deslocamento FROM tb_os WHERE fatura_cod = 0 AND status = 'CONCLUÍDA' ORDER BY data_concluida", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic osFatura = new
                        {
                            Id = Convert.ToInt64(dr["id"]),
                            Data_ordem = Convert.ToDateTime(dr["data_ordem"]),
                            Referencia = Convert.ToString(dr["referencia"]),
                            Profissional_cod = Convert.ToString(dr["profissional_cod"]),
                            Atividade_cod = Convert.ToString(dr["atividade_cod"]),
                            Cidade = Convert.ToString(dr["cidade"]),
                            Nome_cliente = Convert.ToString(dr["nome_cliente"]),
                            Data_vistoria = Convert.ToDateTime(dr["data_vistoria"]),
                            Data_concluida = Convert.ToDateTime(dr["data_concluida"]),
                            Valor_atividade = Convert.ToDouble(dr["valor_atividade"]),
                            Valor_deslocamento = Convert.ToDouble(dr["valor_deslocamento"])
                        };

                        osArray.Add(osFatura);
                    }
                    conexao.Close();
                    return osArray;
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

        public ArrayList GetFiltered(string filtro)
        {
            try
            {
                ArrayList osArray = new ArrayList();
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand(filtro, conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Os os = new Os();

                        os.Id = Convert.ToInt64(dr["id"]);
                        os.Referencia = Convert.ToString(dr["referencia"]);
                        os.Agencia = Convert.ToString(dr["agencia"]);
                        os.Titulo = Convert.ToString(dr["titulo"]);
                        os.DataOrdem = Util.DateTimeToShortDateString(Convert.ToString(dr["data_ordem"]));
                        os.Prazo = Convert.ToDateTime(dr["prazo_execucao"]);
                        os.ProfissionalId = Convert.ToString(dr["profissional_cod"]);
                        os.AtividadeId = Convert.ToString(dr["atividade_cod"]);
                        os.ValorAtividade = Convert.ToString(dr["valor_atividade"]);
                        os.ValorDeslocamento = Convert.ToString(dr["valor_deslocamento"]);
                        os.Siopi = Convert.ToBoolean(dr["siopi"]);
                        os.ClienteNome = Convert.ToString(dr["nome_cliente"]);
                        os.Cidade = Convert.ToString(dr["cidade"]);
                        os.ContatoNome = Convert.ToString(dr["nome_contato"]);
                        os.ContatoTelefone = Convert.ToString(dr["telefone_contato"]);
                        os.Coordenada = Convert.ToString(dr["coordenada"]);
                        os.Status = Convert.ToString(dr["status"]);
                        os.DataPendente = Util.DateTimeToShortDateString(Convert.ToString(dr["data_pendente"]));
                        os.DataVistoria = Util.DateTimeToShortDateString(Convert.ToString(dr["data_vistoria"]));
                        os.DataConcluida = Util.DateTimeToShortDateString(Convert.ToString(dr["data_concluida"]));
                        os.Observacoes = Convert.ToString(dr["obs"]);
                        os.FaturaId = Convert.ToInt64(dr["fatura_cod"]);
                        osArray.Add(os);
                    }
                    conexao.Close();
                    return osArray;
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

        public ArrayList GetProfissionaisDaFatura(long fatura_cod)
        {
            try
            {
                ArrayList profissionaisArray = new ArrayList();

                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT DISTINCT t1.profissional_cod, t2.nomeid FROM tb_os t1 INNER JOIN tb_profissionais t2 on t1.profissional_cod = t2.codigo WHERE t1.fatura_cod = @fatura_cod ORDER BY t2.nomeid", conexao);
                sql.Parameters.AddWithValue("@fatura_cod", fatura_cod);

                MySqlDataReader dr = sql.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic profissionais = new
                        {
                            Codigo = Convert.ToString(dr["profissional_cod"]),
                            Nomeid = Convert.ToString(dr["nomeid"])
                        };

                        profissionaisArray.Add(profissionais);

                    }
                    conexao.Close();
                    return profissionaisArray;
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

        public ArrayList GetCidadesDasOrdens()
        {
            try
            {
                ArrayList cidadesArray = new ArrayList();

                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT DISTINCT cidade FROM tb_os ORDER BY cidade", conexao);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dynamic cidade = new
                        {
                            Cidade = Convert.ToString(dr["cidade"])
                        };

                        cidadesArray.Add(cidade);
                    }
                    conexao.Close();
                    return cidadesArray;
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

        public Os GetBy(long id)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("SELECT * FROM tb_os WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    Os os = new Os();
                    if (dr.Read())
                    {
                        os.Id = Convert.ToInt64(dr["id"]);
                        os.Referencia = Convert.ToString(dr["referencia"]);
                        os.Agencia = Convert.ToString(dr["agencia"]);
                        os.Titulo = Convert.ToString(dr["titulo"]);
                        os.DataOrdem = Convert.ToString(dr["data_ordem"]);
                        os.Prazo = Convert.ToDateTime(dr["prazo_execucao"]);
                        os.ProfissionalId = Convert.ToString(dr["profissional_cod"]);
                        os.AtividadeId = Convert.ToString(dr["atividade_cod"]);
                        os.ValorAtividade = Convert.ToString(dr["valor_atividade"]);
                        os.ValorDeslocamento = Convert.ToString(dr["valor_deslocamento"]);
                        os.Siopi = Convert.ToBoolean(dr["siopi"]);
                        os.ClienteNome = Convert.ToString(dr["nome_cliente"]);
                        os.Cidade = Convert.ToString(dr["cidade"]);
                        os.ContatoNome = Convert.ToString(dr["nome_contato"]);
                        os.ContatoTelefone = Convert.ToString(dr["telefone_contato"]);
                        os.Coordenada = Convert.ToString(dr["coordenada"]);
                        os.Status = Convert.ToString(dr["status"]);
                        os.DataPendente = Convert.ToString(dr["data_pendente"]);
                        os.DataVistoria = Convert.ToString(dr["data_vistoria"]);
                        os.DataConcluida = Convert.ToString(dr["data_concluida"]);
                        os.Observacoes = Convert.ToString(dr["obs"]);
                        os.FaturaId = Convert.ToInt64(dr["fatura_cod"]);
                    }

                    if (os.DataOrdem == "01/01/0001 00:00:00")
                        os.DataOrdem = string.Empty;

                    if (os.DataPendente == "01/01/0001 00:00:00")
                        os.DataPendente = string.Empty;

                    if (os.DataVistoria == "01/01/0001 00:00:00")
                        os.DataVistoria = string.Empty;

                    if (os.DataConcluida == "01/01/0001 00:00:00")
                        os.DataConcluida = string.Empty;

                    conexao.Close();
                    return os;
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

        public long Insert(Os dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_os(titulo, referencia, agencia, data_ordem, prazo_execucao, profissional_cod, atividade_cod, valor_atividade, valor_deslocamento, siopi, nome_cliente, cidade, nome_contato, telefone_contato, coordenada, status, data_pendente, data_vistoria, data_concluida, obs) VALUES (@titulo, @referencia, @agencia, @data_ordem, @prazo_execucao, @profissional_cod, @atividade_cod, @valor_atividade, @valor_deslocamento, @siopi, @nome_cliente, @cidade, @nome_contato, @telefone_contato, @coordenada, @status, @data_pendente, @data_vistoria, @data_concluida, @obs)", conexao);

                sql.Parameters.AddWithValue("@titulo", dado.Titulo);
                sql.Parameters.AddWithValue("@referencia", dado.Referencia);
                sql.Parameters.AddWithValue("@agencia", dado.Agencia);
                sql.Parameters.AddWithValue("@data_ordem", Util.DateOrNull(dado.DataOrdem));
                sql.Parameters.AddWithValue("@prazo_execucao", dado.Prazo);
                sql.Parameters.AddWithValue("@profissional_cod", dado.ProfissionalId);
                sql.Parameters.AddWithValue("@atividade_cod", dado.AtividadeId);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ValorAtividade);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.ValorDeslocamento);
                sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                sql.Parameters.AddWithValue("@nome_cliente", dado.ClienteNome);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@nome_contato", dado.ContatoNome);
                sql.Parameters.AddWithValue("@telefone_contato", dado.ContatoTelefone);
                sql.Parameters.AddWithValue("@coordenada", dado.Coordenada);
                sql.Parameters.AddWithValue("@status", dado.Status);
                sql.Parameters.AddWithValue("@data_pendente", Util.DateOrNull(dado.DataPendente));
                sql.Parameters.AddWithValue("@data_vistoria", Util.DateOrNull(dado.DataVistoria));
                sql.Parameters.AddWithValue("@data_concluida", Util.DateOrNull(dado.DataConcluida));
                sql.Parameters.AddWithValue("@obs", dado.Observacoes);

                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(long id, Os dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_os SET titulo = @titulo, data_ordem = @data_ordem, prazo_execucao = @prazo_execucao, profissional_cod = @profissional_cod, atividade_cod = @atividade_cod, valor_atividade = valor_atividade, valor_deslocamento = valor_deslocamento, siopi = @siopi, nome_cliente = @nome_cliente, cidade = @cidade, nome_contato = @nome_contato, telefone_contato = @telefone_contato, coordenada = @coordenada, status = @status, data_pendente = @data_pendente, data_vistoria = @data_vistoria, data_concluida = @data_concluida, obs = @obs WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@titulo", dado.Titulo);
                sql.Parameters.AddWithValue("@data_ordem", Util.DateOrNull(dado.DataOrdem));
                sql.Parameters.AddWithValue("@prazo_execucao", dado.Prazo);
                sql.Parameters.AddWithValue("@profissional_cod", dado.ProfissionalId);
                sql.Parameters.AddWithValue("@atividade_cod", dado.AtividadeId);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ValorAtividade);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.ValorDeslocamento);
                sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                sql.Parameters.AddWithValue("@nome_cliente", dado.ClienteNome);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@nome_contato", dado.ContatoNome);
                sql.Parameters.AddWithValue("@telefone_contato", dado.ContatoTelefone);
                sql.Parameters.AddWithValue("@coordenada", dado.Coordenada);
                sql.Parameters.AddWithValue("@status", dado.Status);
                sql.Parameters.AddWithValue("@data_pendente", Util.DateOrNull(dado.DataPendente));
                sql.Parameters.AddWithValue("@data_vistoria", Util.DateOrNull(dado.DataVistoria));
                sql.Parameters.AddWithValue("@data_concluida", Util.DateOrNull(dado.DataConcluida));
                sql.Parameters.AddWithValue("@obs", dado.Observacoes);
                sql.Parameters.AddWithValue("@id", id);
                sql.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateFaturaCod(long id, long fatura_cod)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_os SET fatura_cod = @fatura_cod WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@fatura_cod", fatura_cod);
                sql.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateStatus(long id, string status)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand();
                string changeDate = "";

                switch (status)
                {
                    case "RECEBIDA":
                        break;

                    case "PENDENTE":
                        changeDate = ", data_pendente = @data";
                        break;

                    case "VISTORIADA":
                        changeDate = ", data_vistoria = @data";
                        break;

                    case "CONCLUÍDA":
                        changeDate = ", data_concluida = @data";
                        break;
                }

                sql = new MySqlCommand($"UPDATE tb_os SET status = @status {changeDate} WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@status", status);
                sql.Parameters.AddWithValue("@data", DateTime.Now);
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
                MySqlCommand sql = new MySqlCommand("DELETE FROM tb_os WHERE id = @id", conexao);
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