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
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceRepository(connection).GetAll();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Service result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/service")]
        public IActionResult Post([FromBody] Service service)
        {
            try
            {
                _authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new ServiceRepository(connection).Insert(service);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service/{id}")]
        public IActionResult Put([FromBody] Service service)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ServiceRepository(connection).Update(service);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/service/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                {
                    var service = new ServiceRepository(connection).Get(id);

                    if (service.Id != 0)
                        deleted = new ServiceRepository(connection).Delete(service);
                }

                return deleted == false ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
