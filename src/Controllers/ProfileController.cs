using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfileController : ControllerBase
    {
        Autentication Authenticator;

        public ProfileController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/Profile
        public IActionResult GetAll()
        {
            Profile result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).Get(1);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Logo")] // GET:api/Profile/Logo
        public IActionResult GetLogo()
        {
            byte[] result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetLogo();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("ToPrint")] // GET:api/Profile/ToPrint
        public IActionResult GetToPrint()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetToPrint();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("TradingName")] // GET:api/Profile/TradingName
        public IActionResult GetTradingName()
        {
            string result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfileRepository(connection).GetTradingName();
                
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost] // POST:api/Profile
        public IActionResult Post([FromBody] Profile profile)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfileRepository(connection).Insert(profile);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("Logo")] // PUT:api/Profile/Logo
        public IActionResult Put([FromBody] byte[] logo)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfileRepository(connection).UpdateLogo(logo);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut] // PUT:api/Profile
        public IActionResult Put([FromBody] Profile profile)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfileRepository(connection).Update(profile);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
