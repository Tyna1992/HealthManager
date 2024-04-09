using HealthManagerServer.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteCocktail(int id)
    {
        var cocktail = await _context.Cocktails.FirstOrDefaultAsync(cocktail => cocktail.Id == id);
        if (cocktail != null)
        {
        _context.Cocktails.Remove(cocktail);
         await  _context.SaveChangesAsync();
            
        }
    }
    

    public async Task<IEnumerable<Cocktail>> GetAll()
    {
        return await _context.Cocktails.ToListAsync();
    }

    public IList<Cocktail> GetByName(string name)
    {
        return _context.Cocktails.Where(c => c.Name.Contains(name)).ToList();
    }

    public async Task UpdateCocktail(int id, Cocktail cocktail)
    {
        var cocktailToUpdate = await _context.Cocktails.FirstOrDefaultAsync(cocktail => cocktail.Id == id);
        if (cocktailToUpdate != null)
        {
            cocktailToUpdate.Name = cocktail.Name;
            cocktailToUpdate.Ingredients = cocktail.Ingredients;
            cocktailToUpdate.Instructions = cocktail.Instructions;
            _context.Cocktails.Update(cocktailToUpdate);
            await _context.SaveChangesAsync();
        }
    }
    

}