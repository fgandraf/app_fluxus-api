using System.Collections;
using Microsoft.AspNetCore.Mvc;
using FluxusApi.Entities;
using FluxusApi.Repositories;
using MySql.Data.MySqlClient;

namespace FluxusApi.Controllers
{

    [ApiController]
    public class ServiceOrderController : ControllerBase
    {
        private readonly Authentication _authenticator;

        public ServiceOrderController(IHttpContextAccessor context)
            => _authenticator = new Authentication(context);


        [HttpGet("v1/services-orders/flow")]
        public IActionResult GetOrdersFlow()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetOrdersFlow();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services-orders/cities")]
        public IActionResult GetOrderedCities()
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetOrderedCities();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services-orders/done-to-invoice")]
        public IActionResult GetDoneToInvoice() 
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetDoneToInvoice();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services-orders/filtered/{filter}")]
        public IActionResult GetFiltered(string filter)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetFiltered(filter);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services-orders/invoiced/{invoiceId}")]
        public IActionResult GetInvoiced(int invoiceId)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetInvoiced(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/services-orders/professionals/{invoiceId}")]
        public IActionResult GetProfessionals(int invoiceId)
        {
            try
            {
                IEnumerable result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).GetProfessional(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-order/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ServiceOrder result;
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    result = new ServiceOrderRepository(connection).Get(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/service-order")]
        public IActionResult Post([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                _authenticator.Authenticate();

                long id;
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    id = new ServiceOrderRepository(connection).Insert(serviceOrder);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-order")]
        public IActionResult Put([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                _authenticator.Authenticate();

                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ServiceOrderRepository(connection).Update(serviceOrder);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-order/update-invoice/{id},{invoice_id}")]
        public IActionResult UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ServiceOrderRepository(connection).UpdateInvoiceId(id, invoiceId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-order/update-status/{id},{status}")]
        public IActionResult UpdateStatus(int id, EnumStatus status)
        {
            try
            {
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
                    new ServiceOrderRepository(connection).UpdateStatus(id, status);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/service-order/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = false;
                _authenticator.Authenticate();
                
                using (var connection = new MySqlConnection(_authenticator.ConnectionString))
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
