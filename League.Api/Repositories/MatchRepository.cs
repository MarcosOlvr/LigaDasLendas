using Camille.RiotGames;
using Camille.RiotGames.MatchV5;
using League.Api.Repositories.Contracts;

namespace League.Api.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        RiotGamesApi riotApi;

        public MatchRepository()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
        }

        public Match GetMatchById(string id)
        {
            var match = riotApi.MatchV5().GetMatch(Camille.Enums.RegionalRoute.AMERICAS, id);

            return match;
        }

        public string[] GetMatches(string summonerPuuid)
        {
            var lastMaches = riotApi.MatchV5().GetMatchIdsByPUUID(Camille.Enums.RegionalRoute.AMERICAS, summonerPuuid);

            return lastMaches;
        }
    }
}
