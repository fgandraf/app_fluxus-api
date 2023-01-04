using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fluxus.Api.Controllers
{


    [Route("api/[controller]")]


    public class FaturaController : ControllerBase
    {

        Autentication AutenticacaoServico;



        public FaturaController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }



        // GET: api/fatura
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new FaturaRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET api/fatura/getdescricao/<id>
        [HttpGet]
        [Route("getdescricao/{id}")]
        public string GetDescricao(string id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new FaturaRepository().GetDescricaoBy(id);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/fatura/post
        [HttpPost]
        [Route("post")]
        public long Post([FromBody] Fatura fatura)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();
                return new FaturaRepository().Insert(fatura);
            }
            catch
            {
                return 0;
            }

            
        }





        // PUT api/fatura/puttotals/<id>
        [HttpPut]
        [Route("puttotals/{id}")]
        public ReturnAllServices PutTotals(long id, [FromBody] Fatura fatura)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new FaturaRepository().UpdateTotals(id, fatura);

                retorno.Result = true;
                retorno.ErrorMessage = "Fatura Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // DELETE api/fatura/delete/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public ReturnAllServices Delete(string id)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new FaturaRepository().Delete(id);

                retorno.Result = true;
                retorno.ErrorMessage = "Fatura Excluída!";
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
