using Camille.RiotGames.SummonerV4;

namespace League.Api.Repositories.Contracts
{
    public interface IMatchRepository
    {
        Camille.RiotGames.MatchV5.Match GetMatchById(string id);
        string[] GetMatches(string summonerPuuid);
    }
}
