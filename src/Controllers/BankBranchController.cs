using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class BankBranchController : ControllerBase
    {
        Autentication Authenticator;

        public BankBranchController(IHttpContextAccessor context)
        {
            Authenticator = new Autentication(context);
        }


        // GET: api/BankBranch
        [HttpGet]
        public IActionResult GetAll()
        {
            Authenticator.Authenticate();

            var result = new BankBranchRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/BankBranch/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(int id)
        {
            Authenticator.Authenticate();

            var result = new BankBranchRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/BankBranch/Contacts/<branch_number>
        [HttpGet("Contacts/{branch_number}")]
        public IActionResult GetContacts(string branch_number)
        {
            Authenticator.Authenticate();

            var result = new BankBranchRepository().GetContacts(branch_number);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/BankBranch
        [HttpPost]
        public IActionResult Post([FromBody] BankBranch bankBranch)
        {
            Authenticator.Authenticate();

            new BankBranchRepository().Insert(bankBranch);

            return Ok();
        }


        // PUT api/BankBranch/
        [HttpPut]
        public IActionResult Put([FromBody] BankBranch bankBranch)
        {
            Authenticator.Authenticate();

            new BankBranchRepository().Update(bankBranch);

            return Ok();
        }


        // DELETE api/BankBranch/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Authenticator.Authenticate();

            bool deleted = new BankBranchRepository().Delete(id);

            if (deleted)
                return Ok(); 
            else
                return NotFound();
        }
    }
}
