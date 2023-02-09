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


        // GET: api/ServiceOrder/Filtered/<filter><
        [HttpGet("Filtered/{filter}")]
        public IActionResult GetFiltered(string filter)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetFiltered(filter);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/Invoiced/<invoiceId>
        [HttpGet("Invoiced/{invoiceId}")]
        public IActionResult GetInvoiced(int invoiceId)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetInvoiced(invoiceId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/Professionals/<invoiceId>
        [HttpGet("Professionals/{invoiceId}")]
        public IActionResult GetProfessionals(int invoiceId)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetProfessional(invoiceId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(int id)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceOrderRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/ServiceOrder
        [HttpPost]
        public IActionResult Post([FromBody] ServiceOrder serviceOrder)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().Insert(serviceOrder);

            return Ok();
        }


        // PUT api/ServiceOrder/
        [HttpPut]
        public IActionResult Put([FromBody] ServiceOrder serviceOrder)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().Update(serviceOrder);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateInvoiceId/<id>,<invoice_id>
        [HttpPut("UpdateInvoiceId/{id},{invoiceId}")]
        public IActionResult UpdateInvoiceId(int id, int invoiceId)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().UpdateInvoiceId(id, invoiceId);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateStatus/<id>,<status>
        [HttpPut("UpdateStatus/{id},{status}")]
        public IActionResult UpdateStatus(int id, EnumStatus status)
        {
            AutenticacaoServico.Authenticate();

            new ServiceOrderRepository().UpdateStatus(id, status);

            return Ok();
        }


        // DELETE api/ServiceOrder/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
