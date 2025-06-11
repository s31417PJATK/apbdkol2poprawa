using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Models;

[Table("Backpack")]
[PrimaryKey(nameof(Character),nameof(Item))]
public class Backpack
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }
    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    
    public int Amount { get; set; }
    
    public virtual Character Character { get; set; }
    public virtual Item Item { get; set; }
    
}