﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetDescription(string id)
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


        // PUT api/Invoice/Totals/<id>
        [HttpPut("Totals/{id}")]
        public IActionResult PutTotals(long id, [FromBody] Invoice invoice)
        {
            AutenticacaoServico.Authenticate();

            new InvoiceRepository().UpdateTotals(id, invoice);

            return Ok();
        }


        // DELETE api/Invoice/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            AutenticacaoServico.Authenticate();

            bool deleted = new InvoiceRepository().Delete(id);

            if (deleted)
                return Ok(); 
            else
                return NotFound();
        }
    }
}
