using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Models;

[Table("Character_Title")]
[PrimaryKey(nameof(Character),nameof(Title))]
public class Character_Title
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }
    [ForeignKey(nameof(Title))]
    public int TitleId { get; set; }
    
    public DateTime AcquiredAt { get; set; }
    
    public virtual Character Character { get; set; }
    public virtual Title Title { get; set; }
}