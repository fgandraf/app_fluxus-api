using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FluxusApi.Controllers
{

    [Route("api/[controller]")]

    public class ServiceOrderController : ControllerBase
    {
        Autentication Authenticator;

        public ServiceOrderController(IHttpContextAccessor context)
            => Authenticator = new Autentication(context);


        [HttpGet("OrdersFlow")] // GET:api/ServiceOrder/OrdersFlow
        public IActionResult GetOrdersFlow()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetOrdersFlow();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("OrderedCities")] // GET:api/ServiceOrder/OrderedCities
        public IActionResult GetOrderedCities()
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetOrderedCities();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("DoneToInvoice")]// GET:api/ServiceOrder/DoneToInvoice
        public IActionResult GetDoneToInvoice() 
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetDoneToInvoice();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Filtered/{filter}")] // GET:api/ServiceOrder/Filtered/<filter>
        public IActionResult GetFiltered(string filter)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetFiltered(filter);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Invoiced/{invoiceId}")] // GET:api/ServiceOrder/Invoiced/<invoiceId>
        public IActionResult GetInvoiced(int invoiceId)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetInvoiced(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("Professionals/{invoiceId}")] // GET:api/ServiceOrder/Professionals/<invoiceId>
        public IActionResult GetProfessionals(int invoiceId)
        {
            IEnumerable result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).GetProfessional(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")] // GET:api/ServiceOrder/<id>
        public IActionResult Get(int id)
        {
            ServiceOrder result;

            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    result = new ServiceOrderRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost] // POST:api/ServiceOrder
        public IActionResult Post([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).Insert(serviceOrder);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut] // PUT:api/ServiceOrder/
        public IActionResult Put([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                Authenticator.Authenticate();

                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).Update(serviceOrder);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("UpdateInvoiceId/{id},{invoiceId}")] // PUT:api/ServiceOrder/UpdateInvoiceId/<id>,<invoice_id>
        public IActionResult UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).UpdateInvoiceId(id, invoiceId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("UpdateStatus/{id},{status}")] // PUT:api/ServiceOrder/UpdateStatus/<id>,<status>
        public IActionResult UpdateStatus(int id, EnumStatus status)
        {
            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).UpdateStatus(id, status);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")] // DELETE:api/ServiceOrder/<id>
        public IActionResult Delete(int id)
        {
            bool deleted = false;

            try
            {
                Authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                {
                    var serviceOrder = new ServiceOrderRepository(connection).Get(id);

                    if (serviceOrder.Id != 0)
                        deleted = new ServiceOrderRepository(connection).Delete(serviceOrder);

                    return deleted == false ? NotFound() : Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
