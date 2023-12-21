using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Controllers;

[ApiController]
[Route("v1/services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;
    private readonly bool _authenticated;

    public ServiceController(IHttpContextAccessor context, IServiceRepository serviceRepository)
    {
        _authenticated = new Authenticator(context).Authenticate();
        _serviceRepository = serviceRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var result = await _serviceRepository.GetAllAsync();
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
            if (!_authenticated)
                return BadRequest();
                
            var result = await _serviceRepository.GetAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Service service)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            var id = await _serviceRepository.InsertAsync(service);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Service service)
    {
        try
        {
            if (!_authenticated)
                return BadRequest();
                
            await _serviceRepository.UpdateAsync(service);
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
                
            var service = await _serviceRepository.GetAsync(id);

            if (service.Id == 0)
                return NotFound();
                
            await _serviceRepository.DeleteAsync(service);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}