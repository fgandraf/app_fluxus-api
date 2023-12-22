using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileRepository _profileRepository;

    public ProfileController(IProfileRepository profileRepository)
        => _profileRepository = profileRepository;
    

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _profileRepository.GetAsync(1);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("logo")]
    public async Task<IActionResult> GetLogo()
    {
        try
        {
            var result = await _profileRepository.GetLogoAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("to-print")]
    public async Task<IActionResult> GetToPrint()
    {
        try
        {
            var result = await _profileRepository.GetToPrintAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("trading-name")]
    public async Task<IActionResult> GetTradingName()
    {
        try
        {
            var result = await _profileRepository.GetTradingNameAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Profile profile)
    {
        try
        {
            var id = await _profileRepository.InsertAsync(profile);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut("logo")]
    public async Task<IActionResult> Put([FromBody] byte[] logo)
    {
        try
        {
            await _profileRepository.UpdateLogoAsync(logo);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Profile profile)
    {
        try
        {
            await _profileRepository.UpdateAsync(profile);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}