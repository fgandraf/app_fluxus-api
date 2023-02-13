using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ProfessionalController : ControllerBase
    {
        Autentication Authenticator;

        public ProfessionalController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/Professional
        public IActionResult GetAll()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfessionalRepository(connection).GetIndex();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("TagNameId")] // GET:api/Professional/TagNameId
        public IActionResult GetTagNameid()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfessionalRepository(connection).GetTagNameid();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("UserInfo/{userName}")] // GET:api/Professional/UserInfo/<userName>
        public IActionResult GetUserInfo(string userName)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfessionalRepository(connection).GetUserInfoBy(userName);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpGet("{id}")] // GET:api/Professional/<id>
        public IActionResult Get(int id)
        {
            Professional result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ProfessionalRepository(connection).Get(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost] // POST:api/Professional
        public IActionResult Post([FromBody] Professional professional)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfessionalRepository(connection).Insert(professional);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut] // PUT:api/Professional
        public IActionResult Put([FromBody] Professional professional)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ProfessionalRepository(connection).Update(professional);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpDelete("{id}")] // DELETE:api/Professional/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                {
                    var professional = new ProfessionalRepository(connection).Get(id);

                    if (professional.Id != 0)
                        deleted = new ProfessionalRepository(connection).Delete(professional);
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
