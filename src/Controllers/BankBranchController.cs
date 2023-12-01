using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [ApiController]
    public class BankBranchController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public BankBranchController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/bank-branches")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new BankBranchRepository(connection).GetIndex();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branch/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                BankBranch result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new BankBranchRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branch/contacts/{id}")]
        public IActionResult GetContacts(string id)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new BankBranchRepository(connection).GetContacts(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/bank-branch")]
        public IActionResult Post([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new BankBranchRepository(connection).Insert(bankBranch);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/bank-branch")]
        public IActionResult Put([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new BankBranchRepository(connection).Update(bankBranch);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }


        [HttpDelete("v1/bank-branch/{id}")]
        public IActionResult Delete(string id)
        {
            bool deleted = false;

            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                {
                    var bankBranch = new BankBranchRepository(connection).Get(id);

                    if (bankBranch.Id != null)
                        deleted = new BankBranchRepository(connection).Delete(bankBranch);
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
