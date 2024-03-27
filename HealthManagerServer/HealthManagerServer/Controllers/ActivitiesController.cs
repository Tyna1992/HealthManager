using HealthManagerServer.Service;
using HealthManagerServer.Service.ExternalApis;
using HealthManagerServer.Service.JsonProcess;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

public class ActivitiesController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly IActivityRepository _activityRepository;
    private readonly ActivitiesApiCall _activitiesApiCall;
    private readonly JsonProcessor _jsonProcessor;
    
    public ActivitiesController(ILogger<ActivitiesController> logger, IActivityRepository activityRepository,
        ActivitiesApiCall activitiesApiCall, JsonProcessor jsonProcessor)
    {
        _logger = logger;
        _activityRepository = activityRepository;
        _activitiesApiCall = activitiesApiCall;
        _jsonProcessor = jsonProcessor;
    }
    
    [HttpGet("/api/activities/{activityName}/{weight}/{duration}")]
    public async Task<IActionResult> SearchOrAddActivity(string activityName, int weight = 160, int duration=60)
    {
        try
        {
            var result = _activityRepository.GetByActivityName(activityName, weight, duration);
            if (result.Count == 0)
            {
                var activityData = await _activitiesApiCall.GetActivitiesData(activityName, weight, duration);
                var activities = _jsonProcessor.ProcessActivityJson(activityData, weight);
                foreach (var activity in activities)
                {
                    _activityRepository.AddActivity(activity);
                }
                
                return Ok(activities);
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}