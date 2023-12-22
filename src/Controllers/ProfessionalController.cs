using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/professionals")]
public class ProfessionalController : ControllerBase
{
    private readonly IProfessionalRepository _professionalRepository;

    public ProfessionalController(IProfessionalRepository professionalRepository)
        => _professionalRepository = professionalRepository;
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
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