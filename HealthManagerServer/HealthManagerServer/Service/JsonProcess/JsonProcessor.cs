using System.Text.Json;

namespace HealthManagerServer.Service.JsonProcess;

public class JsonProcessor
{

    public Nutrition ProcessNutritionJson(string data)
    {
        JsonDocument document = JsonDocument.Parse(data);
        JsonElement root = document.RootElement;
        Console.WriteLine(root);
        var name = root[0].GetProperty("name").GetString();
        var calories = root[0].GetProperty("calories").GetDouble();
        var servingSize = root[0].GetProperty("serving_size_g").GetDouble();
        var fatTotal = root[0].GetProperty("fat_total_g").GetDouble();
        var fatSaturated = root[0].GetProperty("fat_saturated_g").GetDouble();
        var protein = root[0].GetProperty("protein_g").GetDouble();
        var sodium = root[0].GetProperty("sodium_mg").GetDouble();
        var potassium = root[0].GetProperty("potassium_mg").GetDouble();
        var cholesterol = root[0].GetProperty("cholesterol_mg").GetDouble();
        var carbohydratesTotal = root[0].GetProperty("carbohydrates_total_g").GetDouble();
        var fiber = root[0].GetProperty("fiber_g").GetDouble();
        var sugar = root[0].GetProperty("sugar_g").GetDouble();

        return new Nutrition
        {
            Name = name,
            Calories = calories,
            Serving_size_g = servingSize,
            Fat_total_g = fatTotal,
            Fat_saturated_g = fatSaturated,
            Protein_g = protein,
            Sodium_mg = sodium,
            Potassium_mg = potassium,
            Cholesterol_mg = cholesterol,
            Carbohydrates_total_g = carbohydratesTotal,
            Fiber_g = fiber,
            Sugar_g = sugar

        };

    }
}