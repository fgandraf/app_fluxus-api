using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Fluxus.Api.Entities;
using Fluxus.Api.Repositories;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Fluxus.Api.Controllers
{


    [Route("api/[controller]")]


    public class CadastraisController : ControllerBase
    {

        Autentication AutenticacaoServico;



        public CadastraisController(IHttpContextAccessor context)
        {

            AutenticacaoServico = new Autentication(context);
        }



        // GET: api/cadastrais
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new CadastraisRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }

        }


        // GET: api/cadastrais/getlogo
        [HttpGet]
        [Route("getlogo")]
        public string GetLogo()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                byte[] pass = new CadastraisRepository().GetLogo();
                
                return Convert.ToBase64String(pass);
            }
            catch (Exception)
            {
                throw;
            }

        }


        // GET api/cadastrais/gettoprint
        [HttpGet]
        [Route("gettoprint")]
        public ArrayList GetToPrint()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new CadastraisRepository().GetToPrint();
            }
            catch (Exception)
            {
                throw;
            }

        }






        // GET api/cadastrais/getfantasia
        [HttpGet]
        [Route("getfantasia")]
        public string GetFantasia()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new CadastraisRepository().GetFantasia();
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/cadastrais/post
        [HttpPost]
        [Route("post")]
        public ReturnAllServices Post([FromBody] Cadastrais cadastrais)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new CadastraisRepository().Insert(cadastrais);

                retorno.Result = true;
                retorno.ErrorMessage = "Dados Cadastrais Cadastrados!";

            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErrorMessage = ex.Message;
            }

            return retorno;
        }




        // PUT api/cadastrais/put
        [HttpPut]
        [Route("put")]
        public ReturnAllServices Put([FromBody] Cadastrais cadastrais)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new CadastraisRepository().Update(cadastrais);

                retorno.Result = true;
                retorno.ErrorMessage = "Dados Cadastrais Alterados!";
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
