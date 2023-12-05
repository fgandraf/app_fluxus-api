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
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new BankBranchRepository(connection).GetIndexAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branches/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                BankBranch result = await new BankBranchRepository(connection).GetAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/bank-branches/contacts/{id}")]
        public async Task<IActionResult> GetContactsById(string id)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                IEnumerable result = await new BankBranchRepository(connection).GetContactsAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/bank-branches")]
        public async Task<IActionResult> Post([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new BankBranchRepository(connection).InsertAsync(bankBranch);
                
                return Ok(bankBranch.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/bank-branches")]
        public async Task<IActionResult> Put([FromBody] BankBranch bankBranch)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new BankBranchRepository(connection).UpdateAsync(bankBranch);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }


        [HttpDelete("v1/bank-branches/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _authenticator.Authenticate();
                
                bool deleted = false;
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var bankBranch = await new BankBranchRepository(connection).GetAsync(id);

                if (bankBranch.Id != null)
                    deleted = await new BankBranchRepository(connection).DeleteAsync(bankBranch);

                return deleted == false ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}