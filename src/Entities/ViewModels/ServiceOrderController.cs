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


        [HttpGet("v1/service-orders/flow")]
        public async Task<IActionResult> GetOrdersFlow()
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetOrdersFlowAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/cities")]
        public async Task<IActionResult> GetOrderedCities()
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetOrderedCitiesAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/done-to-invoice")]
        public async Task<IActionResult> GetDoneToInvoice() 
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetDoneToInvoiceAsync();

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/filtered/{filter}")]
        public async Task<IActionResult> GetFiltered(string filter)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetFilteredAsync(filter);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/invoiced/{invoiceId}")]
        public async Task<IActionResult> GetInvoiced(int invoiceId)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetInvoicedAsync(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/professionals/{invoiceId}")]
        public async Task<IActionResult> GetProfessionals(int invoiceId)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetProfessionalAsync(invoiceId);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("v1/service-orders/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var result = await new ServiceOrderRepository(connection).GetAsync(id);

                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("v1/service-orders")]
        public async Task<IActionResult> Post([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                _authenticator.Authenticate();
                
                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var id = await new ServiceOrderRepository(connection).InsertAsync(serviceOrder);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-orders")]
        public async Task<IActionResult> Put([FromBody] ServiceOrder serviceOrder)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ServiceOrderRepository(connection).UpdateAsync(serviceOrder);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-orders/update-invoice/{id},{invoice_id}")]
        public async Task<IActionResult> UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ServiceOrderRepository(connection).UpdateInvoiceIdAsync(id, invoiceId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("v1/service-orders/update-status/{id},{status}")]
        public async Task<IActionResult> UpdateStatus(int id, EnumStatus status)
        {
            try
            {
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                await new ServiceOrderRepository(connection).UpdateStatusAsync(id, status);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("v1/service-orders/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = false;
                _authenticator.Authenticate();

                await using var connection = new MySqlConnection(_authenticator.ConnectionString);
                var serviceOrder = await new ServiceOrderRepository(connection).GetAsync(id);

                if (serviceOrder.Id != 0)
                    deleted = await new ServiceOrderRepository(connection).DeleteAsync(serviceOrder);

                return deleted == false ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
