using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fluxus.Api.Controllers
{


    [Route("api/[controller]")]


    public class OsController : ControllerBase
    {

        Autentication AutenticacaoServico;



        public OsController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }




        // GET: api/os/getordensdofluxo
        [HttpGet]
        [Route("getordensdofluxo")]
        public ArrayList GetOrdensDoFluxo()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetOrdensDoFluxo();
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET: api/os/getcidadesdasordens
        [HttpGet]
        [Route("getcidadesdasordens")]
        public ArrayList GetCidadesDasOrdens()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetCidadesDasOrdens();
            }
            catch (Exception)
            {
                throw;
            }

        }




        // GET: api/os/getordensconcluidasafaturar
        [HttpGet]
        [Route("getordensconcluidasafaturar")]
        public ArrayList GetOrdensConcluidasAFaturar()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetOrdensConcluidasAFaturar();
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET: api/os/getfiltered/<filtro>
        [HttpGet]
        [Route("getfiltered/{filtro}")]
        public ArrayList GetFiltered(string filtro)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetFiltered(filtro);
            }
            catch (Exception)
            {
                throw;
            }

        }




        // GET api/os/getordensfaturadas/<fatura_cod>
        [HttpGet]
        [Route("getordensfaturadas/{fatura_cod}")]
        public ArrayList GetOrdensFaturadas(int fatura_cod)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetOrdensFaturadasBy(fatura_cod);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET api/os/getprofissionaisdafatura/<fatura_cod>
        [HttpGet]
        [Route("getprofissionaisdafatura/{fatura_cod}")]
        public ArrayList GetProfissionaisDaFatura(int fatura_cod)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetProfissionaisDaFatura(fatura_cod);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET api/os/getby/<id>
        [HttpGet]
        [Route("getby/{id}")]
        public Os GetBy(long id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new OsRepository().GetBy(id);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/os/post
        [HttpPost]
        [Route("post")]
        public ReturnAllServices Post([FromBody] Os os)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new OsRepository().Insert(os);

                retorno.Result = true;
                retorno.ErrorMessage = "O.S. Cadastrada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // PUT api/os/put/<id>
        [HttpPut]
        [Route("put/{id}")]
        public ReturnAllServices Put(long id, [FromBody] Os os)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new OsRepository().Update(id, os);

                retorno.Result = true;
                retorno.ErrorMessage = "O.S. Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // PUT api/os/updatefaturacod/<id>,<fatura_cod>
        [HttpPut]
        [Route("updatefaturacod/{id},{fatura_cod}")]
        public ReturnAllServices UpdateFaturaCod(long id, long fatura_cod)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new OsRepository().UpdateFaturaCod(id, fatura_cod);

                retorno.Result = true;
                retorno.ErrorMessage = "O.S. Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }




        // PUT api/os/updatestatus/<id>,<status>
        [HttpPut]
        [Route("updatestatus/{id},{status}")]
        public ReturnAllServices UpdateStatus(long id, string status)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new OsRepository().UpdateStatus(id, status);

                retorno.Result = true;
                retorno.ErrorMessage = "O.S. Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // DELETE api/os/delete/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public ReturnAllServices Delete(long id)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new OsRepository().Delete(id);

                retorno.Result = true;
                retorno.ErrorMessage = "O.S. Excluída!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;

        }


    }


}
