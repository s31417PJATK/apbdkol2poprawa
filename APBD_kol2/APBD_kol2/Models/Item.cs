using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_kol2.Models;

[Table("Item")]
public class Item
{
    [Key]
    public int ItemId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public int Weight { get; set; }
    
    public virtual ICollection<Backpack> Backpacks { get; set; }
}