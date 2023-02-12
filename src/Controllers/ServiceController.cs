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
        Autentication Authenticator;

        public ServiceController(IHttpContextAccessor context)
        {
            Authenticator = new Autentication(context);
        }


        // GET: api/Service
        [HttpGet]
        public IActionResult GetAll()
        {
            Authenticator.Authenticate();

            var result = new ServiceRepository().GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET api/Service/<id>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Authenticator.Authenticate();

            var result = new ServiceRepository().Get(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/Service
        [HttpPost]
        public IActionResult Post([FromBody] Service service)
        {
            Authenticator.Authenticate();

            new ServiceRepository().Insert(service);

            return Ok();
        }


        // PUT api/Service/<id>
        [HttpPut]
        public IActionResult Put([FromBody] Service service)
        {
            Authenticator.Authenticate();

            new ServiceRepository().Update(service);

            return Ok();
        }


        // DELETE api/Service/<id>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Authenticator.Authenticate();

            var service = new ServiceRepository().Get(id);
            bool deleted = false;

            if (service.Id != 0)
                deleted = new ServiceRepository().Delete(service);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
