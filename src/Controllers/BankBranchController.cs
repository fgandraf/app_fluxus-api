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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = await new BankBranchRepository(connection).GetIndexAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branch/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                BankBranch result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = await new BankBranchRepository(connection).GetAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branch/contacts/{id}")]
        public async Task<IActionResult> GetContacts(string id)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = await new BankBranchRepository(connection).GetContactsAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/bank-branch")]
        public async Task<IActionResult> Post([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    await new BankBranchRepository(connection).InsertAsync(bankBranch);
                
                return Ok(bankBranch.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/bank-branch")]
        public async Task<IActionResult> Put([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    await new BankBranchRepository(connection).UpdateAsync(bankBranch);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }


        [HttpDelete("v1/bank-branch/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool deleted = false;

            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                {
                    var bankBranch = await new BankBranchRepository(connection).GetAsync(id);

                    if (bankBranch.Id != null)
                        deleted = await new BankBranchRepository(connection).DeleteAsync(bankBranch);
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
