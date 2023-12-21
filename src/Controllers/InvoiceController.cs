using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Controllers;

[ApiController]
[Route("v1/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly bool _authenticated;

    public InvoiceController(IHttpContextAccessor context, IInvoiceRepository invoiceRepository)
    {
        _authenticated = new Authenticator(context).Authenticate();
        _invoiceRepository = invoiceRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _invoiceRepository.GetAllAsync();
            return result == null ? NotFound() : Ok(((List<Invoice>)result).OrderBy(x => x.IssueDate));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("description/{id}")]
    public async Task<IActionResult> GetDescription(int id)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _invoiceRepository.GetDescriptionAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Invoice invoice)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var id = await _invoiceRepository.InsertAsync(invoice);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut("totals")]
    public async Task<IActionResult> PutTotals([FromBody] Invoice invoice)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            await _invoiceRepository.UpdateTotalsAsync(invoice);
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
            if (!_authenticated)
                return BadRequest();
                
            var invoice = await _invoiceRepository.GetAsync(id);

            if (invoice.Id == 0)
                return NotFound();
                
            await _invoiceRepository.DeleteAsync(invoice);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}