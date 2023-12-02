using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public InvoiceController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/invoices")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = await new InvoiceRepository(connection).GetAllAsync();
                
                return result == null ? NotFound() : Ok(((List<Invoice>)result).OrderBy(x => x.IssueDate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/invoices/description/{id}")]
        public async Task<IActionResult> GetDescription(int id)
        {
            try
            {
                string result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = await new InvoiceRepository(connection).GetDescriptionAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/invoices")]
        public async Task<IActionResult> Post([FromBody] Invoice invoice)
        {
            try
            {
                _authenticator.Authenticate();
                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = await new InvoiceRepository(connection).InsertAsync(invoice);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/invoices/totals")]
        public async Task<IActionResult> PutTotals([FromBody] Invoice invoice)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    await new InvoiceRepository(connection).UpdateTotalsAsync(invoice);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/invoices/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                {
                    var invoice = new InvoiceRepository(connection).Get(id);

                    if (invoice.Id != 0)
                        deleted = await new InvoiceRepository(connection).DeleteAsync(invoice);
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
