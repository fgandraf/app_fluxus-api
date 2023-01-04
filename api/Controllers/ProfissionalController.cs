using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;

namespace Fluxus.Api.Controllers
{


    [Route("api/[controller]")]


    public class ProfissionalController : ControllerBase
    {


        Autentication AutenticacaoServico;



        public ProfissionalController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }




        // GET: api/profissional
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new ProfissionalRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }

        }





        // GET api/profissional/getcodigoenomeid
        [HttpGet]
        [Route("getcodigoenomeid")]
        public ArrayList GetCodigoENomeid()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new ProfissionalRepository().GetCodigoENomeid();
            }
            catch (Exception)
            {
                throw;
            }

        }






        // GET api/profissional/getuserinfo/<userName>
        [HttpGet]
        [Route("getuserinfo/{userName}")]
        public ArrayList GetUserInfo(string userName)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new ProfissionalRepository().GetUserInfoBy(userName);
            }
            catch (Exception)
            {
                throw;
            }

        }




        // GET api/profissional/getby/<id>
        [HttpGet]
        [Route("getby/{id}")]
        public Profissional GetBy(long id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new ProfissionalRepository().GetBy(id);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/profissional/post
        [HttpPost]
        [Route("post")]
        public ReturnAllServices Post([FromBody] Profissional profissional)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new ProfissionalRepository().Insert(profissional);

                retorno.Result = true;
                retorno.ErrorMessage = "Profissional Cadastrado!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // PUT api/profissional/put/<id>
        [HttpPut]
        [Route("put/{id}")]
        public ReturnAllServices Put(long id, [FromBody] Profissional profissional)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new ProfissionalRepository().Update(id, profissional);

                retorno.Result = true;
                retorno.ErrorMessage = "Profissional Alterado!";
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }





        // DELETE api/profissional/delete/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public ReturnAllServices Delete(long id)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new ProfissionalRepository().Delete(id);

                retorno.Result = true;
                retorno.ErrorMessage = "Profissional Excluído!";
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
