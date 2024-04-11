namespace HealthManagerServer.Service.ExternalApis;

public class CocktailApiCall
{
    private readonly string _apiKey;

    public CocktailApiCall()
    {
        _apiKey = "pe8bDjzt2qs1AeJNbzukUw==sXVqA6FU8t4IBEFG";
    }

    public async Task<string> GetCocktailData(string cocktailName)
    {
        string url = $"https://api.api-ninjas.com/v1/cocktail/?name={cocktailName}";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return content;
        }
        return null;
    }
}