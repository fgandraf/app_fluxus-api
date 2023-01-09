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


        // GET: api/ServiceOrder/GetOrdensDoFluxo
        [HttpGet("GetOrdensDoFluxo")]
        public IActionResult GetOrdensDoFluxo()
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetOrdensDoFluxo();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/GetCidadesDasOrdens
        [HttpGet("GetCidadesDasOrdens")]
        public IActionResult GetCidadesDasOrdens()
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetCidadesDasOrdens();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/GetOrdensConcluidasAFaturar
        [HttpGet("GetOrdensConcluidasAFaturar")]
        public IActionResult GetOrdensConcluidasAFaturar()
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetOrdensConcluidasAFaturar();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/ServiceOrder/GetFiltered/<filtro>
        [HttpGet("GetFiltered/{filtro}")]
        public IActionResult GetFiltered(string filtro)
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetFiltered(filtro);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/GetOrdensFaturadas/<fatura_cod>
        [HttpGet("GetOrdensFaturadas/{fatura_cod}")]
        public IActionResult GetOrdensFaturadas(int fatura_cod)
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetOrdensFaturadasBy(fatura_cod);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/GetProfissionaisDaFatura/<fatura_cod>
        [HttpGet("GetProfissionaisDaFatura/{fatura_cod}")]
        public IActionResult GetProfissionaisDaFatura(int fatura_cod)
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetProfissionaisDaFatura(fatura_cod);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/ServiceOrder/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            AutenticacaoServico.Autenticar();

            var result = new ServiceOrderRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/ServiceOrder
        [HttpPost]
        public IActionResult Post([FromBody] ServiceOrder os)
        {
            AutenticacaoServico.Autenticar();

            new ServiceOrderRepository().Insert(os);

            return Ok();
        }


        // PUT api/ServiceOrder/<id>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] ServiceOrder os)
        {
            AutenticacaoServico.Autenticar();

            new ServiceOrderRepository().Update(id, os);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateFaturaCod/<id>,<fatura_cod>
        [HttpPut("UpdateFaturaCod/{id},{fatura_cod}")]
        public IActionResult UpdateFaturaCod(long id, long fatura_cod)
        {
            AutenticacaoServico.Autenticar();

            new ServiceOrderRepository().UpdateFaturaCod(id, fatura_cod);

            return Ok();
        }


        // PUT api/ServiceOrder/UpdateStatus/<id>,<status>
        [HttpPut("UpdateStatus/{id},{status}")]
        public IActionResult UpdateStatus(long id, string status)
        {
            AutenticacaoServico.Autenticar();

            new ServiceOrderRepository().UpdateStatus(id, status);

            return Ok();
        }


        // DELETE api/ServiceOrder/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            AutenticacaoServico.Autenticar();

            bool deleted = new ServiceOrderRepository().Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
