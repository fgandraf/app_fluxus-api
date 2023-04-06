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
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetIndex();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("TagNameId")] // GET:api/Professional/TagNameId
        public IActionResult GetTagNameid()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetTagNameid();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("UserInfo/{userName}")] // GET:api/Professional/UserInfo/<userName>
        public IActionResult GetUserInfo(string userName)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetUserInfoBy(userName);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")] // GET:api/Professional/<id>
        public IActionResult Get(int id)
        {
            Professional result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost] // POST:api/Professional
        public IActionResult Post([FromBody] Professional professional)
        {
            try
            {
                Authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    id = new ProfessionalRepository(connection).Insert(professional);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut] // PUT:api/Professional
        public IActionResult Put([FromBody] Professional professional)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                    new ProfessionalRepository(connection).Update(professional);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")] // DELETE:api/Professional/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(Authenticator.ConnectionString))
                {
                    var professional = new ProfessionalRepository(connection).Get(id);

                    if (professional.Id != 0)
                        deleted = new ProfessionalRepository(connection).Delete(professional);
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

