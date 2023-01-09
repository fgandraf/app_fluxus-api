using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using Microsoft.AspNetCore.Http;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ServiceOrderController : ControllerBase
    {
        Autentication AutenticacaoServico;
        public ServiceOrderController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }


        // GET: api/ServiceOrder/OrdersFlow
        [HttpGet("OrdersFlow")]
        public IActionResult GetOrdersFlow()
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetOrdersFlow();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/OrderedCities
        [HttpGet("OrderedCities")]
        public IActionResult GetOrderedCities()
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetOrderedCities();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/DoneToInvoice
        [HttpGet("DoneToInvoice")]
        public IActionResult GetDoneToInvoice()
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetDoneToInvoice();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/Filtered/<parameters>
        [HttpGet("Filtered/{parameters}")]
        public IActionResult GetFiltered(string parameters)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetFiltered(parameters);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/Invoiced/<invoice_id>
        [HttpGet("Invoiced/{invoice_id}")]
        public IActionResult GetInvoiced(int invoice_id)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetInvoiced(invoice_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/Professionals/<fatura_cod>
        [HttpGet("Professionals/{fatura_cod}")]
        public IActionResult GetProfessionals(int fatura_cod)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetProfessionals(fatura_cod);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/ServiceOrder
        [HttpPost]
        public IActionResult Post([FromBody] ServiceOrder os)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().Insert(os);

            return Ok();
        }


        // PUT api/ServiceOrder/<id>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] ServiceOrder os)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().Update(id, os);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateInvoiceId/<id>,<invoice_id>
        [HttpPut("UpdateInvoiceId/{id},{invoice_id}")]
        public IActionResult UpdateInvoiceId(long id, long invoice_id)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().UpdateInvoiceId(id, invoice_id);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateStatus/<id>,<status>
        [HttpPut("UpdateStatus/{id},{status}")]
        public IActionResult UpdateStatus(long id, string status)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().UpdateStatus(id, status);

            return Ok();
        }


        // DELETE api/ServiceOrder/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            AutenticacaoServico.Authenticate();

            bool deleted = new ServiceOrderRepository().Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
