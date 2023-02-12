using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfileController : ControllerBase
    {
        Autentication Authenticator;

        public ProfileController(IHttpContextAccessor context)
        {

            Authenticator = new Autentication(context);
        }


        // GET: api/Profile
        [HttpGet]
        public IActionResult GetAll()
        {
            Authenticator.Authenticate();

            var result = new ProfileRepository().Get(1);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Profile/Logo
        [HttpGet("Logo")]
        public IActionResult GetLogo()
        {
            Authenticator.Authenticate();

            var result = new ProfileRepository().GetLogo();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Profile/ToPrint
        [HttpGet("ToPrint")]
        public IActionResult GetToPrint()
        {
            Authenticator.Authenticate();

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
            Authenticator.Authenticate();

            var result = new ProfileRepository().GetTradingName();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Profile
        [HttpPost]
        public IActionResult Post([FromBody] Profile profile)
        {
            Authenticator.Authenticate();

            new ProfileRepository().Insert(profile);

            return Ok();
        }


        // PUT api/Profile
        [HttpPut]
        public IActionResult Put([FromBody] Profile profile)
        {
            Authenticator.Authenticate();

            new ProfileRepository().Update(profile);
            
            return Ok();
        }
    }
}
