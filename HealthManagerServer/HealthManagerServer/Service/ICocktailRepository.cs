public interface ICocktailRepository
{
    Task<IEnumerable<Cocktail>> GetAll();
    IList<Cocktail> GetByName(string name);
    void AddCocktail(Cocktail cocktail);
    Task DeleteCocktail(int id);
    Task UpdateCocktail(int id, Cocktail cocktail);
}