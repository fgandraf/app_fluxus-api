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
        public IActionResult GetAll()
        {
            AutenticacaoServico.Autenticar();

            var result = new BankBranchRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/BankBranch/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            AutenticacaoServico.Autenticar();

            var result = new BankBranchRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/BankBranch/GetContacts/<branch_number>
        [HttpGet("GetContacts/{branch_number}")]
        public IActionResult GetSomeBy(string branch_number)
        {
            AutenticacaoServico.Autenticar();

            var result = new BankBranchRepository().GetNamePhoneEmailBy(branch_number);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/BankBranch
        [HttpPost]
        public IActionResult Post([FromBody] BankBranch bankBranch)
        {
            AutenticacaoServico.Autenticar();

            new BankBranchRepository().Insert(bankBranch);

            return Ok();
        }


        // PUT api/BankBranch/<id>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] BankBranch bankBranch)
        {
            AutenticacaoServico.Autenticar();

            new BankBranchRepository().Update(id, bankBranch);

            return Ok();
        }


        // DELETE api/BankBranch/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            AutenticacaoServico.Autenticar();

            int result = new BankBranchRepository().Delete(id);

            if (result == 0)
                return NotFound();
            else
                return Ok();
        }


    }
}
