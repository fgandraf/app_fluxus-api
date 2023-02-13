using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using Microsoft.AspNetCore.Http;
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
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceRepository(connection).GetAll();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("{id}")] // GET:api/Service/<id>
        public IActionResult Get(int id)
        {
            Service result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceRepository(connection).Get(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost] // POST:api/Service
        public IActionResult Post([FromBody] Service service)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceRepository(connection).Insert(service);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut] // PUT:api/Service/<id>
        public IActionResult Put([FromBody] Service service)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceRepository(connection).Update(service);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpDelete("{id}")] // DELETE:api/Service/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                {
                    var service = new ServiceRepository(connection).Get(id);

                    if (service.Id != 0)
                        deleted = new ServiceRepository(connection).Delete(service);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return deleted == false ? NotFound() : Ok();
        }
    }
}
