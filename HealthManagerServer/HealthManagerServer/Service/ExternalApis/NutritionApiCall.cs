

public class NutritionApiCall{
    
    private readonly string _apiKey;

    public NutritionApiCall()
    {
        _apiKey = "pe8bDjzt2qs1AeJNbzukUw==sXVqA6FU8t4IBEFG";
    }

    public async Task<string> GetNutritionData(string foodName, double servingSize)
    {
        string url = $"https://api.api-ninjas.com/v1/nutrition/?query=${servingSize}g ${foodName}";
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