using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public ProfileController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/profile")]
        public IActionResult GetAll()
        {
            try
            {
                Profile result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfileRepository(connection).Get(1);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/logo")]
        public IActionResult GetLogo()
        {
            try
            {
                byte[] result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfileRepository(connection).GetLogo();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/to-print")]
        public IActionResult GetToPrint()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfileRepository(connection).GetToPrint();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/trading-name")]
        public IActionResult GetTradingName()
        {
            try
            {
                string result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfileRepository(connection).GetTradingName();
                
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/profile")]
        public IActionResult Post([FromBody] Profile profile)
        {
            try
            {
                _authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new ProfileRepository(connection).Insert(profile);
                
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/profile/logo")]
        public IActionResult Put([FromBody] byte[] logo)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ProfileRepository(connection).UpdateLogo(logo);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/profile")]
        public IActionResult Put([FromBody] Profile profile)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
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
