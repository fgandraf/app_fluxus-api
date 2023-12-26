using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Models.DTO;
using FluxusApi.Models.ViewModels;
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
            var file = await System.IO.File.ReadAllBytesAsync("wwwroot/logo.png");
            var base64Image = Convert.ToBase64String(file);
            return Ok(base64Image);
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
            ProfileToPrintViewModel model = await _profileRepository.GetToPrintAsync();
            var file = await System.IO.File.ReadAllBytesAsync("wwwroot/logo.png");
            model.Logo = file;
            return model == null ? NotFound() : Ok(model);
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
    public async Task<IActionResult> Post([FromBody]ProfileDTO model)
    {
        try
        {
            var id = await _profileRepository.InsertAsync(model);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPut("logo")]
    public async Task<IActionResult> PutLogo([FromBody]LogoViewModel model)
    {
        try
        {
            var bytes = Convert.FromBase64String(model.Base64Image);
            await System.IO.File.WriteAllBytesAsync("wwwroot/logo.png", bytes);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody]ProfileDTO model)
    {
        try
        {
            await _profileRepository.UpdateAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}