using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Controllers
{

    [ApiController]
    [Route("v1/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private readonly bool _authenticated;

        public ProfileController(IHttpContextAccessor context, IProfileRepository profileRepository)
        {
            _authenticated = new Authenticator(context).Authenticate();
            _profileRepository = profileRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
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
                if (!_authenticated)
                    return BadRequest();
                
                await _profileRepository.UpdateAsync(profile);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }

}
