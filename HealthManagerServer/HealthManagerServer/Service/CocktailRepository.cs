using HealthManagerServer.Data;

public class CocktailRepository : ICocktailRepository
{
    private readonly DataBaseContext _context;
    
    public CocktailRepository(DataBaseContext context)
    {
        _context = context;
    }

    public void AddCocktail(Cocktail cocktail)
    {
        _context.Cocktails.Add(cocktail);
        _context.SaveChanges();
    }

    public void DeleteCocktail(Cocktail cocktail)
    {
        _context.Cocktails.Remove(cocktail);
        _context.SaveChanges();
    }

    public IEnumerable<Cocktail> GetAll()
    {
        return _context.Cocktails.ToList();
    }

    public IQueryable<Cocktail> GetByName(string name)
    {
        return _context.Cocktails.Where(c => c.Name.Contains(name));
    }

    public void UpdateCocktail(Cocktail cocktail)
    {
        _context.Cocktails.Update(cocktail);
        _context.SaveChanges();
    }
}