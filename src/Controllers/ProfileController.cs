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
            var file = await System.IO.File.ReadAllBytesAsync("wwwroot/logo.png");
            result.Logo = file;
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
            var file = await System.IO.File.ReadAllBytesAsync("wwwroot/logo.png");
            var bytes = Convert.ToBase64String(file);
            return Ok(bytes);
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
            var file = await System.IO.File.ReadAllBytesAsync("wwwroot/logo.png");
            result.Logo = file;
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
            await System.IO.File.WriteAllBytesAsync("wwwroot/logo.png", profile.Logo);
            profile.Logo = null;
            
            var id = await _profileRepository.InsertAsync(profile);
            return Ok(id);
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
            await System.IO.File.WriteAllBytesAsync("wwwroot/logo.png", profile.Logo);
            profile.Logo = null;
            
            await _profileRepository.UpdateAsync(profile);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}