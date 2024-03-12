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
        var nameString = name.Split("and");
        Nutrition? result;
        IList<Nutrition> nutritionList = new List<Nutrition>();
        IList<Nutrition> alreadyInDatabaseNutrition = new List<Nutrition>();
        try
        {
            for (int i = 0; i < nameString.Length; i++)
            {
                result = _nutritionRepository.GetByNameAndWeight(nameString[i], servingSize);
                if (result == null)
                {
                    var nutritionData = await _nutritionApiCall.GetNutritionData(nameString[i], servingSize);
                    var nutrition = _jsonProcessor.ProcessNutritionJson(nutritionData);
                    _nutritionRepository.AddNutrition(nutrition);
                    nutritionList.Add(nutrition);
                    _logger.LogInformation(nutrition + " added to the database");
                }
                else
                {
                    alreadyInDatabaseNutrition.Add(result);
                }
            }
            
            if (alreadyInDatabaseNutrition.Count > 0 && nutritionList.Count == 0)
            {
                return Ok(alreadyInDatabaseNutrition);
            }
            
            if (alreadyInDatabaseNutrition.Count > 0 && nutritionList.Count > 0)
            {
                return Ok(alreadyInDatabaseNutrition.Concat(nutritionList));
            }
            
            return Ok(nutritionList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    

    
    

}