using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Controllers;

[ApiController]
[Route("v1/professionals")]
public class ProfessionalController : ControllerBase
{
    private readonly IProfessionalRepository _professionalRepository;
    private readonly bool _authenticated;

    public ProfessionalController(IHttpContextAccessor context, IProfessionalRepository professionalRepository)
    {
        _authenticated = new Authenticator(context).Authenticate();
        _professionalRepository = professionalRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _professionalRepository.GetIndexAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("tag-name-id")]
    public async Task<IActionResult> GetTagNameid()
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _professionalRepository.GetTagNameidAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

        
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _professionalRepository.GetAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Professional professional)
    {
        try
        {
                
            if (!_authenticated)
                return BadRequest();
                
            var id = await _professionalRepository.InsertAsync(professional);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Professional professional)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            await _professionalRepository.UpdateAsync(professional);
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
                
            var professional = await _professionalRepository.GetAsync(id);

            if (professional.Id == 0)
                return NotFound();
                
            await _professionalRepository.DeleteAsync(professional);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}