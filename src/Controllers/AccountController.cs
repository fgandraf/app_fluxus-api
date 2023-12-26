using FluxusApi.Models.DTO;
using FluxusApi.Models.ViewModels;
using FluxusApi.Repositories.Contracts;
using FluxusApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/accounts")]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;
    
    
    public AccountController(TokenService tokenService, IUserRepository userRepository)
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
    public async Task<IActionResult> Post([FromBody] UserDTO userDto)
    {
        try
        {
            userDto.UserPassword = PasswordHasher.Hash(userDto.UserPassword);
            
            var id = await _userRepository.InsertAsync(userDto);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UserDTO userDto)
    {
        try
        {
            userDto.UserPassword = PasswordHasher.Hash(userDto.UserPassword);
            
            await _userRepository.UpdateAsync(userDto);
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