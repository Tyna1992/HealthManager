using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthManagerServer.Service.ExternalApis;

namespace HealthManagerServer.Controllers;

[ApiController]
[Route("[controller]")]
public class MealController : ControllerBase
{
    private readonly INutritionRepository _nutritionRepository;
    private readonly NutritionApiCall _nutritionApiCall;
    private readonly JsonProcessor _jsonProcessor;

    public MealController(INutritionRepository userRepository, NutritionApiCall nutritionApiCall, JsonProcessor jsonProcessor)
    {
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

    [HttpGet("/api/meal/getAll"), Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_nutritionRepository.GetAll());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("/api/meal/delete/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _nutritionRepository.DeleteNutrition(id);
            return Ok(new { message = "Deleted" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("/api/meal/update/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Nutrition nutrition)
    {
        try
        {
            await _nutritionRepository.UpdateNutrition(id, nutrition);
            return Ok(new { message = "Updated" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}