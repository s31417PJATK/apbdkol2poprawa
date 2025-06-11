using APBD_kol2.Data;
using APBD_kol2.DTOs;
using APBD_kol2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Services;

public class kol2Service : Ikol2Service
{
    private readonly DatabaseContext _context;

    public kol2Service(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<object> getCharacter(int id)
    {
        var character = await _context.Characters.Where(c => c.CharacterID ==id)
            .Select(c =>
            new GetCharacterDTO()
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b =>
                    new BackPackItemDTO()
                    {
                        Amount = b.Amount,
                        ItemName = b.Item.Name,
                        ItemWeight = b.Item.Weight,
                    }
                ).ToList(),
                Titles = c.Character_Titles.Select(ct =>
                    new TitleDTO()
                    {
                        Title = ct.Title.Name,
                        AcquiredAt = ct.AcquiredAt,
                    }
                ).ToList()
            }).FirstOrDefaultAsync();
        
        if (character == null) return null;
        
        return character;
    }

    public async Task<object> postBackpacks(int id, List<int> items)
    {
        if (items.Count == 0) return "list is empty";
        
        if (_context.Characters.Where(c => c.CharacterID ==id).ToList().Count() == 0 ) return "Character not found";
        
        List<int> weights = new List<int>();
        for (int i = 0; i < items.Count; i++)
        {
            if (_context.Items.Where(it => it.ItemId == items[i]).ToList().Count() == 0) return "Item not found";
            weights.Add(await _context.Items.Where(it => it.ItemId == items[i]).Select(it => it.Weight).FirstOrDefaultAsync());
        }
        
        int sum_items = weights.Sum();
        
        if (await _context.Characters.Where(c => c.CharacterID ==id).Select(c => c.CurrentWeight).FirstOrDefaultAsync() + sum_items > await _context.Characters.Where(c => c.CharacterID ==id).Select(c => c.MaxWeight).FirstOrDefaultAsync()) return "Too much weight";
        
        var character = await _context.Characters.Where(c => c.CharacterID == id).FirstOrDefaultAsync();
        character.CurrentWeight += sum_items;

        for (int i = 0; i < items.Count; i++)
        {
            if (_context.Backpacks.Where(b => b.CharacterId == id && b.ItemId == items[i]).ToList().Count() == 0)
                _context.Backpacks.Add(new Backpack() { CharacterId = id, ItemId = items[i], Amount = 1 });
            else
            {
                var backpack = await _context.Backpacks.Where(b => b.CharacterId == id && b.ItemId == items[i]).FirstOrDefaultAsync();
                backpack.Amount += 1;
            }
        }
        
        
        await _context.SaveChangesAsync();
        return "added";
    }
}