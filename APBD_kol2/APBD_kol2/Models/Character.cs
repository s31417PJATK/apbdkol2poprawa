using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_kol2.Models;

[Table("Character")]
public class Character
{
    [Key]
    public int CharacterID { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }
    
    public int CurrentWeight { get; set; }
    
    public int MaxWeight { get; set; }
    
    public virtual ICollection<Backpack> Backpacks { get; set; }
    public virtual ICollection<Character_Title> Character_Titles { get; set; }
}