using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfileController : ControllerBase
    {
        Autentication AutenticacaoServico;

        public ProfileController(IHttpContextAccessor context)
        {

            AutenticacaoServico = new Autentication(context);
        }


        // GET: api/Profile
        [HttpGet]
        public IActionResult GetAll()
        {
            AutenticacaoServico.Autenticar();

            var result = new ProfileRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Profile/GetLogo
        [HttpGet("GetLogo")]
        public IActionResult GetLogo()
        {
            AutenticacaoServico.Autenticar();

            var result = new ProfileRepository().GetLogo();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Profile/GetToPrint
        [HttpGet("GetToPrint")]
        public IActionResult GetToPrint()
        {
            AutenticacaoServico.Autenticar();

            var result = new ProfileRepository().GetToPrint();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Profile/GetTradingName
        [HttpGet]
        [Route("GetTradingName")]
        public IActionResult GetTradingName()
        {
            AutenticacaoServico.Autenticar();

            var result = new ProfileRepository().GetTradingName();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Profile
        [HttpPost]
        public IActionResult Post([FromBody] Profile profile)
        {
            AutenticacaoServico.Autenticar();

            new ProfileRepository().Insert(profile);

            return Ok();
        }


        // PUT api/Profile
        [HttpPut]
        public IActionResult Put([FromBody] Profile profile)
        {
            AutenticacaoServico.Autenticar();

            new ProfileRepository().Update(profile);
            
            return Ok();
        }
    }
}
