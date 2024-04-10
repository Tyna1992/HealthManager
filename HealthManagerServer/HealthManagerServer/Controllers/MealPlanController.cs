using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.JsonProcess;
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
    public async Task<IActionResult> CreateMealPlan([FromBody] MealPlan mealPlan)
    {
        
        try
        {
            await _mealPlanRepository.AddMealPlan(mealPlan);
            return Ok(mealPlan + "Meal plan created successfully");
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