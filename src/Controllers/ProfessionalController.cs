using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfessionalController : ControllerBase
    {
        Autentication Authenticator;

        public ProfessionalController(IHttpContextAccessor context)
        {
            Authenticator = new Autentication(context);
        }


        // GET: api/Professional
        [HttpGet]
        public IActionResult GetAll()
        {
            Authenticator.Authenticate();

            var result = new ProfessionalRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/TagNameId
        [HttpGet("TagNameId")]
        public IActionResult GetTagNameid()
        {
            Authenticator.Authenticate();

            var result = new ProfessionalRepository().GetTagNameid();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/UserInfo/<userName>
        [HttpGet("UserInfo/{userName}")]
        public IActionResult GetUserInfo(string userName)
        {
            Authenticator.Authenticate();

            var result = new ProfessionalRepository().GetUserInfoBy(userName);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Professional/<id>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Authenticator.Authenticate();

            var result = new ProfessionalRepository().Get(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Professional
        [HttpPost]
        public IActionResult Post([FromBody] Professional professional)
        {
            Authenticator.Authenticate();

            new ProfessionalRepository().Insert(professional);

            return Ok();
        }


        // PUT api/Professional
        [HttpPut]
        public IActionResult Put([FromBody] Professional professional)
        {
            Authenticator.Authenticate();

            new ProfessionalRepository().Update(professional);

            return Ok();
        }


        // DELETE api/Professional/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Authenticator.Authenticate();

            var professional = new ProfessionalRepository().Get(id);
            bool deleted = false;

            if (professional.Id != 0)
                deleted = new ProfessionalRepository().Delete(professional);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
