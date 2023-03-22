using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;
using System.Collections;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class BankBranchController : ControllerBase
    {
        Autentication Authenticator;

        public BankBranchController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/BankBranch
        public IActionResult GetAll()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new BankBranchRepository(connection).GetIndex();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")] // GET:api/BankBranch/<id>
        public IActionResult Get(int id)
        {
            BankBranch result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new BankBranchRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Contacts/{branch_number}")] // GET:api/BankBranch/Contacts/<branch_number>
        public IActionResult GetContacts(string branch_number)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new BankBranchRepository(connection).GetContacts(branch_number);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost] // POST:api/BankBranch
        public IActionResult Post([FromBody] BankBranch bankBranch)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new BankBranchRepository(connection).Insert(bankBranch);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut] // PUT:api/BankBranch/
        public IActionResult Put([FromBody] BankBranch bankBranch)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new BankBranchRepository(connection).Update(bankBranch);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }


        [HttpDelete("{id}")] // DELETE:api/BankBranch/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                {
                    var bankBranch = new BankBranchRepository(connection).Get(id);

                    if (bankBranch.Id != 0)
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
