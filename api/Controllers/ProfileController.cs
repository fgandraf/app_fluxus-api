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
            AutenticacaoServico.Authenticate();

            var result = new ProfileRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Profile/Logo
        [HttpGet("Logo")]
        public IActionResult GetLogo()
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfileRepository().GetLogo();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Profile/ToPrint
        [HttpGet("ToPrint")]
        public IActionResult GetToPrint()
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfileRepository().GetToPrint();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Profile/TradingName
        [HttpGet]
        [Route("TradingName")]
        public IActionResult GetTradingName()
        {
            AutenticacaoServico.Authenticate();

            var result = new ProfileRepository().GetTradingName();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Profile
        [HttpPost]
        public IActionResult Post([FromBody] Profile profile)
        {
            AutenticacaoServico.Authenticate();

            new ProfileRepository().Insert(profile);

            return Ok();
        }


        // PUT api/Profile
        [HttpPut]
        public IActionResult Put([FromBody] Profile profile)
        {
            AutenticacaoServico.Authenticate();

            new ProfileRepository().Update(profile);
            
            return Ok();
        }
    }
}
