public interface ICocktailRepository
{
    IEnumerable<Cocktail> GetAll();
    Cocktail? GetByName(string name);
    void AddCocktail(Cocktail cocktail);
    void DeleteCocktail(Cocktail cocktail);
    void UpdateCocktail(Cocktail cocktail);
}