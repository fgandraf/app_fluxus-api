using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;

namespace FluxusApi.Repositories
{


    public class ServiceOrderRepository
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
                        ServiceOrder os = new ServiceOrder();

                        os.Id = Convert.ToInt64(dr["id"]);
                        os.ReferenceCode = Convert.ToString(dr["referencia"]);
                        os.Branch = Convert.ToString(dr["agencia"]);
                        os.Title = Convert.ToString(dr["titulo"]);
                        os.OrderDate = Util.DateTimeToShortDateString(Convert.ToString(dr["data_ordem"]));
                        os.Deadline = Convert.ToDateTime(dr["prazo_execucao"]);
                        os.ProfessionalId = Convert.ToString(dr["profissional_cod"]);
                        os.ServiceId = Convert.ToString(dr["atividade_cod"]);
                        os.ServiceAmount = Convert.ToString(dr["valor_atividade"]);
                        os.MileageAllowance = Convert.ToString(dr["valor_deslocamento"]);
                        os.Siopi = Convert.ToBoolean(dr["siopi"]);
                        os.CustomerName = Convert.ToString(dr["nome_cliente"]);
                        os.City = Convert.ToString(dr["cidade"]);
                        os.ContactName = Convert.ToString(dr["nome_contato"]);
                        os.ContactPhone = Convert.ToString(dr["telefone_contato"]);
                        os.Coordinates = Convert.ToString(dr["coordenada"]);
                        os.Status = Convert.ToString(dr["status"]);
                        os.PendingDate = Util.DateTimeToShortDateString(Convert.ToString(dr["data_pendente"]));
                        os.SurveyDate = Util.DateTimeToShortDateString(Convert.ToString(dr["data_vistoria"]));
                        os.DoneDate = Util.DateTimeToShortDateString(Convert.ToString(dr["data_concluida"]));
                        os.Comments = Convert.ToString(dr["obs"]);
                        os.InvoiceId = Convert.ToInt64(dr["fatura_cod"]);
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

        public ServiceOrder GetBy(long id)
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
                    ServiceOrder os = new ServiceOrder();
                    if (dr.Read())
                    {
                        os.Id = Convert.ToInt64(dr["id"]);
                        os.ReferenceCode = Convert.ToString(dr["referencia"]);
                        os.Branch = Convert.ToString(dr["agencia"]);
                        os.Title = Convert.ToString(dr["titulo"]);
                        os.OrderDate = Convert.ToString(dr["data_ordem"]);
                        os.Deadline = Convert.ToDateTime(dr["prazo_execucao"]);
                        os.ProfessionalId = Convert.ToString(dr["profissional_cod"]);
                        os.ServiceId = Convert.ToString(dr["atividade_cod"]);
                        os.ServiceAmount = Convert.ToString(dr["valor_atividade"]);
                        os.MileageAllowance = Convert.ToString(dr["valor_deslocamento"]);
                        os.Siopi = Convert.ToBoolean(dr["siopi"]);
                        os.CustomerName = Convert.ToString(dr["nome_cliente"]);
                        os.City = Convert.ToString(dr["cidade"]);
                        os.ContactName = Convert.ToString(dr["nome_contato"]);
                        os.ContactPhone = Convert.ToString(dr["telefone_contato"]);
                        os.Coordinates = Convert.ToString(dr["coordenada"]);
                        os.Status = Convert.ToString(dr["status"]);
                        os.PendingDate = Convert.ToString(dr["data_pendente"]);
                        os.SurveyDate = Convert.ToString(dr["data_vistoria"]);
                        os.DoneDate = Convert.ToString(dr["data_concluida"]);
                        os.Comments = Convert.ToString(dr["obs"]);
                        os.InvoiceId = Convert.ToInt64(dr["fatura_cod"]);
                    }

                    if (os.OrderDate == "01/01/0001 00:00:00")
                        os.OrderDate = string.Empty;

                    if (os.PendingDate == "01/01/0001 00:00:00")
                        os.PendingDate = string.Empty;

                    if (os.SurveyDate == "01/01/0001 00:00:00")
                        os.SurveyDate = string.Empty;

                    if (os.DoneDate == "01/01/0001 00:00:00")
                        os.DoneDate = string.Empty;

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

        public long Insert(ServiceOrder dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("INSERT INTO tb_os(titulo, referencia, agencia, data_ordem, prazo_execucao, profissional_cod, atividade_cod, valor_atividade, valor_deslocamento, siopi, nome_cliente, cidade, nome_contato, telefone_contato, coordenada, status, data_pendente, data_vistoria, data_concluida, obs) VALUES (@titulo, @referencia, @agencia, @data_ordem, @prazo_execucao, @profissional_cod, @atividade_cod, @valor_atividade, @valor_deslocamento, @siopi, @nome_cliente, @cidade, @nome_contato, @telefone_contato, @coordenada, @status, @data_pendente, @data_vistoria, @data_concluida, @obs)", conexao);

                sql.Parameters.AddWithValue("@titulo", dado.Title);
                sql.Parameters.AddWithValue("@referencia", dado.ReferenceCode);
                sql.Parameters.AddWithValue("@agencia", dado.Branch);
                sql.Parameters.AddWithValue("@data_ordem", Util.DateOrNull(dado.OrderDate));
                sql.Parameters.AddWithValue("@prazo_execucao", dado.Deadline);
                sql.Parameters.AddWithValue("@profissional_cod", dado.ProfessionalId);
                sql.Parameters.AddWithValue("@atividade_cod", dado.ServiceId);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ServiceAmount);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.MileageAllowance);
                sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                sql.Parameters.AddWithValue("@nome_cliente", dado.CustomerName);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@nome_contato", dado.ContactName);
                sql.Parameters.AddWithValue("@telefone_contato", dado.ContactPhone);
                sql.Parameters.AddWithValue("@coordenada", dado.Coordinates);
                sql.Parameters.AddWithValue("@status", dado.Status);
                sql.Parameters.AddWithValue("@data_pendente", Util.DateOrNull(dado.PendingDate));
                sql.Parameters.AddWithValue("@data_vistoria", Util.DateOrNull(dado.SurveyDate));
                sql.Parameters.AddWithValue("@data_concluida", Util.DateOrNull(dado.DoneDate));
                sql.Parameters.AddWithValue("@obs", dado.Comments);

                sql.ExecuteNonQuery();
                conexao.Close();
                return sql.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(long id, ServiceOrder dado)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ConnectionString.CONNECTION_STRING);
                conexao.Open();
                MySqlCommand sql = new MySqlCommand("UPDATE tb_os SET titulo = @titulo, data_ordem = @data_ordem, prazo_execucao = @prazo_execucao, profissional_cod = @profissional_cod, atividade_cod = @atividade_cod, valor_atividade = valor_atividade, valor_deslocamento = valor_deslocamento, siopi = @siopi, nome_cliente = @nome_cliente, cidade = @cidade, nome_contato = @nome_contato, telefone_contato = @telefone_contato, coordenada = @coordenada, status = @status, data_pendente = @data_pendente, data_vistoria = @data_vistoria, data_concluida = @data_concluida, obs = @obs WHERE id = @id", conexao);
                sql.Parameters.AddWithValue("@titulo", dado.Title);
                sql.Parameters.AddWithValue("@data_ordem", Util.DateOrNull(dado.OrderDate));
                sql.Parameters.AddWithValue("@prazo_execucao", dado.Deadline);
                sql.Parameters.AddWithValue("@profissional_cod", dado.ProfessionalId);
                sql.Parameters.AddWithValue("@atividade_cod", dado.ServiceId);
                sql.Parameters.AddWithValue("@valor_atividade", dado.ServiceAmount);
                sql.Parameters.AddWithValue("@valor_deslocamento", dado.MileageAllowance);
                sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                sql.Parameters.AddWithValue("@nome_cliente", dado.CustomerName);
                sql.Parameters.AddWithValue("@cidade", dado.City);
                sql.Parameters.AddWithValue("@nome_contato", dado.ContactName);
                sql.Parameters.AddWithValue("@telefone_contato", dado.ContactPhone);
                sql.Parameters.AddWithValue("@coordenada", dado.Coordinates);
                sql.Parameters.AddWithValue("@status", dado.Status);
                sql.Parameters.AddWithValue("@data_pendente", Util.DateOrNull(dado.PendingDate));
                sql.Parameters.AddWithValue("@data_vistoria", Util.DateOrNull(dado.SurveyDate));
                sql.Parameters.AddWithValue("@data_concluida", Util.DateOrNull(dado.DoneDate));
                sql.Parameters.AddWithValue("@obs", dado.Comments);
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