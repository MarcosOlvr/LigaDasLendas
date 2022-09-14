using League.Api.Models;

namespace League.Api.Repositories.Contracts
{
    public interface IChampRepository
    {
        List<Champ> GetAllChamps();

        Champ GetChampById(int id);
    }
}
