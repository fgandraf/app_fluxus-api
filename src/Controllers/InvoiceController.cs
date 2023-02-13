using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class InvoiceController : ControllerBase
    {
        Autentication Authenticator;

        public InvoiceController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet] // GET:api/Invoice
        public IActionResult GetAll()
        {
            List<Invoice> result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = (List<Invoice>)new InvoiceRepository(connection).GetAll();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result.OrderBy(x => x.IssueDate));
        }


        [HttpGet("Description/{id}")] // GET:api/Invoice/Description/<id>
        public IActionResult GetDescription(int id)
        {
            string result;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new InvoiceRepository(connection).GetDescription(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost] // POST:api/Invoice
        public IActionResult Post([FromBody] Invoice invoice)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new InvoiceRepository(connection).Insert(invoice);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut("Totals")] // PUT:api/Invoice/Totals/
        public IActionResult PutTotals([FromBody] Invoice invoice)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new InvoiceRepository(connection).UpdateTotals(invoice);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpDelete("{id}")] // DELETE:api/Invoice/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                {
                    var invoice = new InvoiceRepository(connection).Get(id);

                    if (invoice.Id != 0)
                        deleted = new InvoiceRepository(connection).Delete(invoice);
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
