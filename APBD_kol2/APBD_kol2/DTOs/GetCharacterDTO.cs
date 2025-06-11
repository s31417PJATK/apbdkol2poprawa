namespace APBD_kol2.DTOs;

public class GetCharacterDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackPackItemDTO> BackpackItems { get; set; }
    public List<TitleDTO> Titles { get; set; }
}

public class BackPackItemDTO
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitleDTO
{
    public string Title { get; set; }
    public DateTime AcquiredAt { get; set; }
}