using Microsoft.AspNetCore.Mvc;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FluxusApi.Controllers;

[Authorize]
[ApiController]
[Route("v1/bank-branches")]
public class BankBranchController : ControllerBase
{
    private readonly IBankBranchRepository _bankBranchRepository;

    public BankBranchController(IBankBranchRepository bankBranchRepository)
        => _bankBranchRepository = bankBranchRepository;
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _bankBranchRepository.GetIndexAsync();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var result = await _bankBranchRepository.GetAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("contacts/{id}")]
    public async Task<IActionResult> GetContactsById(string id)
    {
        try
        {
            var result = await _bankBranchRepository.GetContactsAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BankBranch bankBranch)
    {
        try
        {
            await _bankBranchRepository.InsertAsync(bankBranch);
            return Ok(bankBranch.Id);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BankBranch bankBranch)
    {
        try
        {
            await _bankBranchRepository.UpdateAsync(bankBranch);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var bankBranch = await _bankBranchRepository.GetAsync(id);

            if (bankBranch.Id == null)
                return NotFound();

            await _bankBranchRepository.DeleteAsync(bankBranch);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}