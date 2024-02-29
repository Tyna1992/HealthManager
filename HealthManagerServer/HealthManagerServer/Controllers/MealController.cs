using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

[ApiController]
[Route("[controller]")]
public class MealController : ControllerBase
{
    private readonly ILogger<MealController> _logger;
    private readonly INutritionRepository _nutritionRepository ;
    private readonly NutritionApiCall _nutritionApiCall;
    private readonly JsonProcessor _jsonProcessor;

    public MealController(ILogger<MealController> logger, INutritionRepository userRepository, NutritionApiCall nutritionApiCall, JsonProcessor jsonProcessor)
    {
        _logger = logger;
        _nutritionRepository = userRepository;
        _nutritionApiCall = nutritionApiCall;
        _jsonProcessor = jsonProcessor;
    }


    [HttpGet("/api/meal/{servingSize}/{name}")]
    public async Task<IActionResult> SearchOrAddMeal(double servingSize, string name)
    {
        try
        {
            var result = _nutritionRepository.GetByNameAndWeight(name, servingSize);
            if (result == null)
            {
                var nutritionData = await _nutritionApiCall.GetNutritionData(name, servingSize);
                var nutrition = _jsonProcessor.ProcessNutritionJson(nutritionData);
                _nutritionRepository.AddNutrition(nutrition);
                return Ok(nutrition);
                
            }
            
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    
    

}