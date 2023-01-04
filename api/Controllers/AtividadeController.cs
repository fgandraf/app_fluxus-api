using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fluxus.Api.Controllers
{
   
    
    [Route("api/[controller]")]


    public class AtividadeController : ControllerBase
    {

        
        Autentication AutenticacaoServico;



        public AtividadeController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }



        // GET: api/atividade
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new AtividadeRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }

            
        }


        // GET api/atividade/getby/<id>
        [HttpGet]
        [Route("getby/{id}")]
        public Atividade GetBy(long id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new AtividadeRepository().GetBy(id);
            }
            catch (Exception)
            {
                throw;
            }
        }




        // POST api/atividade/post
        [HttpPost]
        [Route("post")]
        public ReturnAllServices Post([FromBody] Atividade atividade)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            AtividadeRepository atividadeDAO = new AtividadeRepository();
            try
            {
                AutenticacaoServico.Autenticar();

                new AtividadeRepository().Insert(atividade);

                retorno.Result = true;
                retorno.ErrorMessage = "Atividade Cadastrada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // PUT api/atividade/put/<id>
        [HttpPut]
        [Route("put/{id}")]
        public ReturnAllServices Put(long id, [FromBody] Atividade atividade)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new AtividadeRepository().Update(id, atividade);

                retorno.Result = true;
                retorno.ErrorMessage = "Atividade Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
                
        }





        // DELETE api/atividade/delete/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public ReturnAllServices Delete(long id)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new AtividadeRepository().Delete(id);

                retorno.Result = true;
                retorno.ErrorMessage = "Atividade Excluída!";
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
