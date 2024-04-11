namespace HealthManagerServer.Service.ExternalApis;

public class ActivitiesApiCall
{
    private readonly string _apiKey;

    public ActivitiesApiCall()
    {
        _apiKey = "pe8bDjzt2qs1AeJNbzukUw==sXVqA6FU8t4IBEFG";
    }

    public async Task<string> GetActivitiesData(string activityName, int weight = 75, int duration = 60 )
    {
        int weightInPounds = (int)(weight / 0.45359237);
        string url = $"https://api.api-ninjas.com/v1/caloriesburned/?activity={activityName}&weight={weightInPounds}&duration={duration}";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
        var response = await client.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        return null;
    }
}