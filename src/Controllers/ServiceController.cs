using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public ServiceController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/services")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new ServiceRepository(connection).GetAllAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                Service result = await new ServiceRepository(connection).GetAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/services")]
        public async Task<IActionResult> Post([FromBody] Service service)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                long id = await new ServiceRepository(connection).InsertAsync(service);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/services")]
        public async Task<IActionResult> Put([FromBody] Service service)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ServiceRepository(connection).UpdateAsync(service);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/services/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var service = await new ServiceRepository(connection).GetAsync(id);
                
                if (service.Id != 0)
                    deleted = await new ServiceRepository(connection).DeleteAsync(service);

                return deleted == false ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
