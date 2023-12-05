using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{
    
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public ProfessionalController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/professionals")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new ProfessionalRepository(connection).GetIndexAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professionals/tag-name-id")]
        public async Task<IActionResult> GetTagNameid()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new ProfessionalRepository(connection).GetTagNameidAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professionals/user-info/{userName}")]
        public async Task<IActionResult> GetUserInfo(string userName)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new ProfessionalRepository(connection).GetUserInfoByAsync(userName);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professionals/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                Professional result = await new ProfessionalRepository(connection).GetAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/professionals")]
        public async Task<IActionResult> Post([FromBody] Professional professional)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                long id = await new ProfessionalRepository(connection).InsertAsync(professional);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/professionals")]
        public async Task<IActionResult> Put([FromBody] Professional professional)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ProfessionalRepository(connection).UpdateAsync(professional);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/professionals/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var professional = await new ProfessionalRepository(connection).GetAsync(id);
                
                if (professional.Id != 0)
                    deleted = await new ProfessionalRepository(connection).DeleteAsync(professional);
                

                return deleted == false ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}

