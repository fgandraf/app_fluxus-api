using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Models.Enums;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/service-orders")]
public class ServiceOrderController : ControllerBase
{
    private readonly IServiceOrderRepository _serviceOrderRepository;

    public ServiceOrderController(IHttpContextAccessor context, IServiceOrderRepository serviceOrderRepository)
        => _serviceOrderRepository = serviceOrderRepository;
    

    [HttpGet("flow")]
    public async Task<IActionResult> GetOrdersFlow()
    {
        try
        {
            var result = await _serviceOrderRepository.GetOrdersFlowAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("cities")]
    public async Task<IActionResult> GetOrderedCities()
    {
        try
        {
            var result = await _serviceOrderRepository.GetOrderedCitiesAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("done-to-invoice")]
    public async Task<IActionResult> GetDoneToInvoice() 
    {
        try
        {
            var result = await _serviceOrderRepository.GetDoneToInvoiceAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("filtered/{filter}")]
    public async Task<IActionResult> GetFiltered(string filter)
    {
        try
        {
            var result = await _serviceOrderRepository.GetFilteredAsync(filter);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("invoiced/{invoiceId}")]
    public async Task<IActionResult> GetInvoiced(int invoiceId)
    {
        try
        {
            var result = await _serviceOrderRepository.GetInvoicedAsync(invoiceId);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("professionals/{invoiceId}")]
    public async Task<IActionResult> GetProfessionals(int invoiceId)
    {
        try
        {
            var result = await _serviceOrderRepository.GetProfessionalAsync(invoiceId);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _serviceOrderRepository.GetAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ServiceOrder serviceOrder)
    {
        try
        {
            var id = await _serviceOrderRepository.InsertAsync(serviceOrder);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ServiceOrder serviceOrder)
    {
        try
        {
            await _serviceOrderRepository.UpdateAsync(serviceOrder);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
        
        
    [HttpPut("update-invoice/{invoiceId}")]
    public async Task<IActionResult> UpdateInvoiceId(int invoiceId, [FromBody]List<int> orders)
    {
        try
        {
            await _serviceOrderRepository.UpdateInvoiceIdAsync(invoiceId, orders);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut("update-status/{id},{status}")]
    public async Task<IActionResult> UpdateStatus(int id, EnumStatus status)
    {
        try
        {
            await _serviceOrderRepository.UpdateStatusAsync(id, status);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var serviceOrder = await _serviceOrderRepository.GetAsync(id);

            if (serviceOrder.Id == 0)
                return NotFound();
                
            await _serviceOrderRepository.DeleteAsync(serviceOrder);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}