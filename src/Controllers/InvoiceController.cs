using Microsoft.AspNetCore.Mvc;
using FluxusApi.Repositories;
using FluxusApi.Entities;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class InvoiceController : ControllerBase
    {
        Autentication AutenticacaoServico;

        public InvoiceController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }


        // GET: api/Invoice
        [HttpGet]
        public IActionResult GetAll()
        {
            AutenticacaoServico.Authenticate();

            var result = new InvoiceRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Invoice/Description/<id>
        [HttpGet("Description/{id}")]
        public IActionResult GetDescription(int id)
        {
            AutenticacaoServico.Authenticate();

            var result = new InvoiceRepository().GetDescription(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Invoice
        [HttpPost]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            AutenticacaoServico.Authenticate();

            new InvoiceRepository().Insert(invoice);

            return Ok();
        }


        // PUT api/Invoice/Totals/
        [HttpPut("Totals")]
        public IActionResult PutTotals([FromBody] Invoice invoice)
        {
            AutenticacaoServico.Authenticate();

            new InvoiceRepository().UpdateTotals(invoice);

            return Ok();
        }


        // DELETE api/Invoice/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AutenticacaoServico.Authenticate();

            int rowsAffected = new InvoiceRepository().Delete(id);

            if (rowsAffected > 0)
                return Ok(); 
            else
                return NotFound();
        }
    }
}
