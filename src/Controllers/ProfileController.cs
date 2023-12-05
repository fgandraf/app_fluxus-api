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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                Profile result = await new ProfileRepository(connection).GetAsync(1);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/logo")]
        public async Task<IActionResult> GetLogo()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                byte[] result = await new ProfileRepository(connection).GetLogoAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/to-print")]
        public async Task<IActionResult> GetToPrint()
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new ProfileRepository(connection).GetToPrintAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/profile/trading-name")]
        public async Task<IActionResult> GetTradingName()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                string result = await new ProfileRepository(connection).GetTradingNameAsync();
                
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/profile")]
        public async Task<IActionResult> Post([FromBody] Profile profile)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                long id = await new ProfileRepository(connection).InsertAsync(profile);
                
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/profile/logo")]
        public async Task<IActionResult> Put([FromBody] byte[] logo)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ProfileRepository(connection).UpdateLogoAsync(logo);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/profile")]
        public async Task<IActionResult> Put([FromBody] Profile profile)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ProfileRepository(connection).UpdateAsync(profile);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
