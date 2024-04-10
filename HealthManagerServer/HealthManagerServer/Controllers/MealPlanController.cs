using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MealPlanController : ControllerBase
{
    private readonly IMealPlanRepository _mealPlanRepository;
    private readonly INutritionRepository _nutritionRepository;
    private readonly NutritionApiCall _nutritionApiCall;
    private readonly JsonProcessor _jsonProcessor;
    private readonly ILogger<MealPlanController> _logger;

    public MealPlanController(IMealPlanRepository mealPlanRepository, INutritionRepository nutritionRepository, JsonProcessor jsonProcessor, NutritionApiCall nutritionApiCall, ILogger<MealPlanController> logger)
    {
        _mealPlanRepository = mealPlanRepository;
        _nutritionRepository = nutritionRepository;
        _jsonProcessor = jsonProcessor;
        _nutritionApiCall = nutritionApiCall;
        _logger = logger;
    }


    [HttpPost("create")]
    public async Task<ActionResult<MealPlan>> CreateMealPlan([FromBody] MealPlanRequest request)
    {
        try
        {
            if (request.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("Date cannot be in the past");
            }
            var formatedDate = request.Date.ToDateTime(TimeOnly.MinValue);
            var nutrition = _nutritionRepository.GetByNameAndWeight(request.Name, request.ServingSize);
            if (nutrition == null)
            {
                var nutritionData = await _nutritionApiCall.GetNutritionData(request.Name, request.ServingSize);
                var nutritionDataJson = _jsonProcessor.ProcessNutritionJson(nutritionData);
                _nutritionRepository.AddNutrition(nutritionDataJson);
                _logger.LogInformation(nutritionDataJson + " added to the database");
                var mealPlan = new MealPlan()
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    MealId = nutritionDataJson.Id,
                    Date = formatedDate,
                    MealTime = request.MealTime
                };
                await _mealPlanRepository.AddMealPlan(mealPlan);
                _logger.LogInformation(mealPlan + "Meal plan created successfully");
                return Ok(mealPlan);
            }
            else
            {
                var mealPlan = new MealPlan()
                {
                    UserName = request.UserName,
                    MealId = nutrition.Id,
                    Date = formatedDate,
                    MealTime = request.MealTime
                };
                await _mealPlanRepository.AddMealPlan(mealPlan);
                _logger.LogInformation(mealPlan + "Meal plan created successfully");
                return Ok(mealPlan);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getByDate/{date}")]
    public async Task<IActionResult> GetByDate(DateOnly date)
    {

        try
        {
            var mealPlan = await _mealPlanRepository.GetByDate(date);
            if (mealPlan != null)
            {
                return Ok(mealPlan);
            }
            return NotFound("Meal plan not found on the given date");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}