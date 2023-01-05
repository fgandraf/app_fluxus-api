using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;


namespace FluxusApi.Controllers
{


    [Route("api/[controller]")]


    public class BankBranchController : ControllerBase
    {



        Autentication AutenticacaoServico;



        public BankBranchController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }



        // GET: api/BankBranch
        [HttpGet]
        public ArrayList GetAll()
        {
            try
            {
                AutenticacaoServico.Autenticar();
                
                return new BankBranchRepository().GetAll();
            }
            catch (Exception)
            {
                throw;
            }


        }



        // GET: api/BankBranch/GetBy/<id>
        [HttpGet]
        [Route("GetBy/{id}")]
        public BankBranch GetBy(long id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new BankBranchRepository().GetBy(id);
            }
            catch (Exception)
            {
                throw;
            }

        }




        // GET: api/BankBranch/GetNamePhoneEmailBy/<agenciaCodigo>
        [HttpGet]
        [Route("GetNamePhoneEmailBy/{branch_number}")]
        public ArrayList GetSomeBy(string branch_number)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                return new BankBranchRepository().GetNamePhoneEmailBy(branch_number);
            }
            catch (Exception)
            {
                throw;
            }

        }





        // POST api/BankBranch/Post
        [HttpPost]
        [Route("Post")]
        public ReturnAllServices Post([FromBody] BankBranch bankBranch)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();
                new BankBranchRepository().Insert(bankBranch);

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





        // PUT api/BankBranch/Put/<id>
        [HttpPut]
        [Route("Put/{id}")]
        public ReturnAllServices Put(long id, [FromBody] BankBranch bankBranch)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();

                new BankBranchRepository().Update(id, bankBranch);

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





        // DELETE api/BankBranch/Delete/<id>
        [HttpDelete]
        [Route("Delete/{id}")]
        public ReturnAllServices Delete(long id)
        {

            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                AutenticacaoServico.Autenticar();
                new BankBranchRepository().Delete(id);

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
