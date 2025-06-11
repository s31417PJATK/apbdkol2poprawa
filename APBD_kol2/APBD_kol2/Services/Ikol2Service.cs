using APBD_kol2.DTOs;

namespace APBD_kol2.Services;

public interface Ikol2Service
{
    public Task<object> getCharacter(int id);
    
    public Task<object> postBackpacks(int id,List<int> postBackpackDTO);
}