using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfessionalController : ControllerBase
    {
        Autentication AutenticacaoServico;

        public ProfessionalController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }


        // GET: api/Professional
        [HttpGet]
        public IActionResult GetAll()
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfessionalRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/TagNameId
        [HttpGet("TagNameId")]
        public IActionResult GetTagNameid()
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfessionalRepository().GetTagNameid();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/UserInfo/<userName>
        [HttpGet("UserInfo/{userName}")]
        public IActionResult GetUserInfo(string userName)
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfessionalRepository().GetUserInfoBy(userName);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfessionalRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Professional
        [HttpPost]
        public IActionResult Post([FromBody] Professional professional)
        {
            AutenticacaoServico.Authenticate();

            new ProfessionalRepository().Insert(professional);

            return Ok();
        }


        // PUT api/Professional/<id>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Professional profissional)
        {
            AutenticacaoServico.Authenticate();

            new ProfessionalRepository().Update(id, profissional);

            return Ok();
        }


        // DELETE api/Professional/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            AutenticacaoServico.Authenticate();

            bool deleted = new ProfessionalRepository().Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
