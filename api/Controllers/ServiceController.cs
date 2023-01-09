using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using Microsoft.AspNetCore.Http;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ServiceController : ControllerBase
    {
        Autentication AutenticacaoServico;

        public ServiceController(IHttpContextAccessor context)
        {
            AutenticacaoServico = new Autentication(context);
        }


        // GET: api/Service
        [HttpGet]
        public IActionResult GetAll()
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Service/<id>
        [HttpGet("{id}")]
        public IActionResult GetBy(long id)
        {
            AutenticacaoServico.Authenticate();

            var result = new ServiceRepository().GetBy(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Service
        [HttpPost]
        public IActionResult Post([FromBody] Service atividade)
        {
            AutenticacaoServico.Authenticate();

            new ServiceRepository().Insert(atividade);

            return Ok();
        }


        // PUT api/Service/<id>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Service atividade)
        {
            AutenticacaoServico.Authenticate();

            new ServiceRepository().Update(id, atividade);

            return Ok();
        }


        // DELETE api/Service/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            AutenticacaoServico.Authenticate();

            bool deleted = new ServiceRepository().Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
