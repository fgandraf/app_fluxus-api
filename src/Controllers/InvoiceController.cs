using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class InvoiceController : ControllerBase
    {
        Autentication Authenticator;

        public InvoiceController(IHttpContextAccessor context)
        {
            Authenticator = new Autentication(context);
        }


        // GET: api/Invoice
        [HttpGet]
        public IActionResult GetAll()
        {
            Authenticator.Authenticate();

            var result = (List<Invoice>)new InvoiceRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result.OrderBy(x => x.IssueDate));
        }


        // GET api/Invoice/Description/<id>
        [HttpGet("Description/{id}")]
        public IActionResult GetDescription(int id)
        {
            Authenticator.Authenticate();

            var result = new InvoiceRepository().GetDescription(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Invoice
        [HttpPost]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            Authenticator.Authenticate();

            new InvoiceRepository().Insert(invoice);

            return Ok();
        }


        // PUT api/Invoice/Totals/
        [HttpPut("Totals")]
        public IActionResult PutTotals([FromBody] Invoice invoice)
        {
            Authenticator.Authenticate();

            new InvoiceRepository().UpdateTotals(invoice);

            return Ok();
        }


        // DELETE api/Invoice/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Authenticator.Authenticate();

            var invoice = new InvoiceRepository().Get(id);
            bool deleted = false;

            if (invoice.Id != 0)
                deleted = new InvoiceRepository().Delete(invoice);

            if (deleted)
                return Ok(); 
            else
                return NotFound();
        }
    }
}
