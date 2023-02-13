using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
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
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return result == null ? NotFound() : Ok(result);
        }


        [HttpPost] // POST:api/ServiceOrder
        public IActionResult Post([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).Insert(serviceOrder);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut] // PUT:api/ServiceOrder/
        public IActionResult Put([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).Update(serviceOrder);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut("UpdateInvoiceId/{id},{invoiceId}")] // PUT:api/ServiceOrder/UpdateInvoiceId/<id>,<invoice_id>
        public IActionResult UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).UpdateInvoiceId(id, invoiceId);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
        }


        [HttpPut("UpdateStatus/{id},{status}")] // PUT:api/ServiceOrder/UpdateStatus/<id>,<status>
        public IActionResult UpdateStatus(int id, EnumStatus status)
        {
            try
            {
                Authenticator.Authenticate();
                using (var connection = new MySqlConnection(ConnectionString.Get()))
                    new ServiceOrderRepository(connection).UpdateStatus(id, status);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Ok();
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
