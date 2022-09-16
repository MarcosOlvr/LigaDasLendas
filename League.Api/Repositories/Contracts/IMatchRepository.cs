using Camille.RiotGames.SummonerV4;
using League.Api.Models;

namespace League.Api.Repositories.Contracts
{
    public interface IMatchRepository
    {
        Camille.RiotGames.MatchV5.Match GetMatchById(string id);
        string[] GetMatches(string summonerPuuid);
        Item GetItem(int itemId);
        List<Item> GetItems(
            int item1, int item2, 
            int item3, int item4,
            int item5, int item6);
    }
}
