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


    [HttpPost("create"), Authorize(Roles = "User")]
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

    [HttpGet("getByDate/{userName}/{date}"), Authorize(Roles = "User")]
    public async Task<IActionResult> GetByDate(DateOnly date, string userName)
    {
        try
        {
            var mealPlan = await _mealPlanRepository.GetByDate(date, userName);
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

    [HttpGet("getAll"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllMealPlans()
    {
        try
        {
            var mealPlans = await _mealPlanRepository.GetAllMealPlans();
            return Ok(mealPlans);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getByDay/{userName}/{day}"), Authorize(Roles = "User")]
    public async Task<IActionResult> GetMealPlansByDay(string day, string userName) //day should start with uppercase letter
    {
        try
        {
            var mealPlans = await _mealPlanRepository.GetMealPlansByDay(day, userName);
            if (mealPlans != null)
            {
                return Ok(mealPlans);
            }
            return NotFound("No meal plans found on the given day");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getByUserName/{userName}"), Authorize(Roles = "User")]
    public async Task<IActionResult> GetMealPlansByUserName(string userName)
    {
        try
        {
            var mealPlans = await _mealPlanRepository.GetMealPlansByUserName(userName);
            if (mealPlans != null)
            {
                return Ok(mealPlans);
            }
            return NotFound("No meal plans found for the given user");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update/{id}"), Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateMealPlan(string id, [FromBody] MealPlan mealPlan)
    {
        try
        {
            if (DateOnly.FromDateTime(mealPlan.Date) < DateOnly.FromDateTime(DateTime.Now))
            {
                return BadRequest("Date cannot be in the past");
            }
            await _mealPlanRepository.UpdateMealPlan(id, mealPlan);
            return Ok("Meal plan updated successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}"), Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMealPlan(string id)
    {
        try
        {
            await _mealPlanRepository.DeleteMealPlan(id);
            return Ok("Meal plan deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}