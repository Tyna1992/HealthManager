using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

public class CocktailController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly ICocktailRepository _cocktailRepository;
    private readonly CocktailApiCall _cocktailApiCall;
    private readonly JsonProcessor _jsonProcessor;

    public CocktailController(ILogger<ActivitiesController> logger, ICocktailRepository cocktailRepository,
        CocktailApiCall cocktailApiCall, JsonProcessor jsonProcessor)
    {
        _logger = logger;
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
            if (result == null)
            {
                var cocktailData = await _cocktailApiCall.GetCocktailData(cocktailName);
                var cocktail = _jsonProcessor.ProcessCocktailJson(cocktailData);
                _cocktailRepository.AddCocktail(cocktail);
                return Ok(cocktail);
            }

            return Ok(result); 
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}