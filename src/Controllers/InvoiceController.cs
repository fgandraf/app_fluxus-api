using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceController(IInvoiceRepository invoiceRepository)
        => _invoiceRepository = invoiceRepository;
    

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
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