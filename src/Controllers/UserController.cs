using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using FluxusApi.Services;
using FluxusApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public UserController(TokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel user)
    {
        var userInDb = await _userRepository.GetByUserNameAsync(user.UserName);
        
        if (userInDb == null || userInDb.Id == 0)
            return NotFound("Usuário ou senha inválida!");
        
        if (!userInDb.UserActive)
            return BadRequest("Usuário ou senha inválida!");
        
        if (!PasswordHasher.Verify(userInDb.UserPassword, user.Password))
            return BadRequest("Usuário ou senha inválida!");
        
        var token = _tokenService.GenerateToken(userInDb);
        return Ok(token);
    }
    
    [HttpGet("username/{userName}")]
    public async Task<IActionResult> GetByUsername(string userName)
    {
        try
        {
            var result = await _userRepository.GetByUserNameAsync(userName);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
        
    [HttpGet("professional/{professionalId}")]
    public async Task<IActionResult> GetByProfessionalId(int professionalId)
    {
        try
        {
            var result = await _userRepository.GetByProfessionalIdAsync(professionalId);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        try
        {
            user.UserPassword = PasswordHasher.Hash(user.UserPassword);
            
            var id = await _userRepository.InsertAsync(user);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] User user)
    {
        try
        {
            user.UserPassword = PasswordHasher.Hash(user.UserPassword);
            
            await _userRepository.UpdateAsync(user);
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
            var professional = await _userRepository.GetAsync(id);

            if (professional.Id == 0)
                return NotFound();
                
            await _userRepository.DeleteAsync(professional);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}