using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.ExternalApis;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

public class CocktailController : ControllerBase
{
    private readonly ICocktailRepository _cocktailRepository;
    private readonly CocktailApiCall _cocktailApiCall;
    private readonly JsonProcessor _jsonProcessor;

    public CocktailController(ICocktailRepository cocktailRepository,
        CocktailApiCall cocktailApiCall, JsonProcessor jsonProcessor)
    {
        _cocktailRepository = cocktailRepository;
        _cocktailApiCall = cocktailApiCall;
        _jsonProcessor = jsonProcessor;
    }

    [HttpGet("/api/cocktail/{cocktailName}")]
    public async Task<IActionResult> SearchOrAddCocktail(string cocktailName)
    {
        try
        {
            var result = _cocktailRepository.GetByName(cocktailName);
            if (result.Count == 0)
            {
                var cocktailData = await _cocktailApiCall.GetCocktailData(cocktailName);
                var cocktails = _jsonProcessor.ProcessCocktailJson(cocktailData);

                foreach (var cocktail in cocktails)
                {
                    _cocktailRepository.AddCocktail(cocktail);
                }

                return Ok(cocktails);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/cocktail/getAll"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCocktails()
    {
        try
        {
            var result = await _cocktailRepository.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("/api/cocktail/delete/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCocktail(int id)
    {
        try
        {
            await _cocktailRepository.DeleteCocktail(id);
            return Ok(new { message = "Cocktail deleted" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("/api/cocktail/update/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCocktail(int id, [FromBody] Cocktail cocktail)
    {
        try
        {
            await _cocktailRepository.UpdateCocktail(id, cocktail);
            return Ok(new { message = "Cocktail updated" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}