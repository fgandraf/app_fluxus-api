using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FluxusApi.Controllers
{
    
    [ApiController]
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly bool _authenticated;
    
        public UserController(IHttpContextAccessor context, IUserRepository userRepository)
        {
            _authenticated = new Authenticator(context).Authenticate();
            _userRepository = userRepository;
        }
    
    
        [HttpGet("username/{userName}")]
        public async Task<IActionResult> GetByUsername(string userName)
        {
            try
            {
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
}