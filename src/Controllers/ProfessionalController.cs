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
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetIndex();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professionals/tag-name-id")]
        public IActionResult GetTagNameid()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetTagNameid();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professional/user-info/{userName}")]
        public IActionResult GetUserInfo(string userName)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).GetUserInfoBy(userName);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/professional/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Professional result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ProfessionalRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/professional")]
        public IActionResult Post([FromBody] Professional professional)
        {
            try
            {
                _authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new ProfessionalRepository(connection).Insert(professional);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/professional")]
        public IActionResult Put([FromBody] Professional professional)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ProfessionalRepository(connection).Update(professional);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/professional/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
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

