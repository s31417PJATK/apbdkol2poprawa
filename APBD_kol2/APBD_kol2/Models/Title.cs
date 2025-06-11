using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_kol2.Models;

[Table(("Title"))]
public class Title
{
    [Key]
    public int TitleId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public virtual ICollection<Character_Title> Character_Titles { get; set; }
}