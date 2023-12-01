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
        public IActionResult GetAll()
        {
            try
            {
                List<Invoice> result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = (List<Invoice>)new InvoiceRepository(connection).GetAll();

                return result == null ? NotFound() : Ok(result.OrderBy(x => x.IssueDate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/invoice/description/{id}")]
        public IActionResult GetDescription(int id)
        {
            try
            {
                string result;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new InvoiceRepository(connection).GetDescription(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/invoice")]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            try
            {
                _authenticator.Authenticate();
                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new InvoiceRepository(connection).Insert(invoice);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/invoice/totals")]
        public IActionResult PutTotals([FromBody] Invoice invoice)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new InvoiceRepository(connection).UpdateTotals(invoice);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/invoice/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                {
                    var invoice = new InvoiceRepository(connection).Get(id);

                    if (invoice.Id != 0)
                        deleted = new InvoiceRepository(connection).Delete(invoice);
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
