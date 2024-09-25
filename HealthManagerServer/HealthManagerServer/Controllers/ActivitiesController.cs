using HealthManagerServer.Model;
using HealthManagerServer.Service;
using HealthManagerServer.Service.ExternalApis;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

public class ActivitiesController : ControllerBase
{
    private readonly IActivityRepository _activityRepository;
    private readonly ActivitiesApiCall _activitiesApiCall;
    private readonly JsonProcessor _jsonProcessor;

    public ActivitiesController(IActivityRepository activityRepository,
        ActivitiesApiCall activitiesApiCall, JsonProcessor jsonProcessor)
    {
        _activityRepository = activityRepository;
        _activitiesApiCall = activitiesApiCall;
        _jsonProcessor = jsonProcessor;
    }

    [HttpGet("/api/activities/{activityName}/{weight}/{duration}")]
    public async Task<IActionResult> SearchOrAddActivity(string activityName, int weight = 75, int duration = 60)
    {
            var weightInPounds = (int)(weight * 2.20462262);
            var result = _activityRepository.GetByActivityName(activityName, weightInPounds, duration);
            if (result.Count == 0)
            {
                var activityData = await _activitiesApiCall.GetActivitiesData(activityName, weightInPounds, duration);
                var activities = _jsonProcessor.ProcessActivityJson(activityData, weight);
                foreach (var activity in activities)
                {
                    _activityRepository.AddActivity(activity);
                }

                return Ok(activities);
            }
            return Ok(result);
        
        
    }

    [HttpGet("/api/activities/getAll"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllActivities()
    {
        try
        {
            var result = await _activityRepository.GetAll();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("/api/activities/delete/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _activityRepository.DeleteActivity(id);
            return Ok(new { message = "Deleted" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("/api/activities/update/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Activity activity)
    {
        try
        {
            await _activityRepository.UpdateActivity(id, activity);
            return Ok(new { message = "Updated" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}