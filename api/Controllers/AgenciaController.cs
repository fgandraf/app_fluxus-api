using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;


namespace Fluxus.Api.Controllers
{


    [Route("api/[controller]")]


    public class AgenciaController : ControllerBase
    {



        Autentication AutenticacaoServico;



        public AgenciaController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }



        // GET: api/agencia
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                
                return new AgenciaRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }


        }



        // GET: api/agencia/getby/<id>
        [HttpGet]
        [Route("getby/{id}")]
        public Agencia GetBy(long id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new AgenciaRepository().GetBy(id);
            }
            catch (Exception)
            {
                throw;
            }

        }




        // GET: api/agencia/getsomeby/<agenciaCodigo>
        [HttpGet]
        [Route("getsomeby/{agenciaCodigo}")]
        public ArrayList GetSomeBy(string agenciaCodigo)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new AgenciaRepository().GetNomeTelefone1EmailBy(agenciaCodigo);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/agencia/post
        [HttpPost]
        [Route("post")]
        public ReturnAllServices Post([FromBody] Agencia agencia)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();
                new AgenciaRepository().Insert(agencia);

                retorno.Result = true;
                retorno.ErrorMessage = "Agencia Cadastrada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // PUT api/agencia/put/<id>
        [HttpPut]
        [Route("put/{id}")]
        public ReturnAllServices Put(long id, [FromBody] Agencia agencia)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new AgenciaRepository().Update(id, agencia);

                retorno.Result = true;
                retorno.ErrorMessage = "Agencia Alterada!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // DELETE api/agencia/delete/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public ReturnAllServices Delete(long id)
        {

            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();
                new AgenciaRepository().Delete(id);

                retorno.Result = true;
                retorno.ErrorMessage = "Agencia Excluída!";
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
