public class Cocktail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Ingredients { get; set; }
    public string Instructions { get; set; }
}