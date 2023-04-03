using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ServiceController : ControllerBase
    {
        Autentication Authenticator;

        public ServiceController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/Service
        public IActionResult GetAll()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ServiceRepository(connection).GetAll();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")] // GET:api/Service/<id>
        public IActionResult Get(int id)
        {
            Service result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ServiceRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost] // POST:api/Service
        public IActionResult Post([FromBody] Service service)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    new ServiceRepository(connection).Insert(service);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut] // PUT:api/Service/<id>
        public IActionResult Put([FromBody] Service service)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    new ServiceRepository(connection).Update(service);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")] // DELETE:api/Service/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
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
