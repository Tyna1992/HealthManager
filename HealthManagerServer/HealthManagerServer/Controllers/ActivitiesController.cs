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
    public async Task<IActionResult> SearchOrAddActivity(string activityName, double weight = 160, int duration=60)
    {
        var result = _activityRepository.GetByActivityName(activityName);
        Console.WriteLine("Result:"+result);
        if (result == null)
        {
            var activityData = await _activitiesApiCall.GetActivitiesData(activityName, weight, duration);
            var activity = _jsonProcessor.ProcessActivityJson(activityData);
            _activityRepository.AddActivity(activity);
            return Ok(activity);
        }
        return Ok(result);



            
           


    }
    
}