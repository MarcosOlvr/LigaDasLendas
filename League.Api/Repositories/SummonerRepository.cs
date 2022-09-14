using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.ChampionMasteryV4;
using Camille.RiotGames.SummonerV4;
using League.Api.Repositories.Contracts;
using RiotSharp;

namespace League.Api.Repositories
{
    public class SummonerRepository : ISummonerRepository
    {
        RiotGamesApi riotApi;

        public SummonerRepository()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
        }

        public ChampionMastery[] GetChampMastery(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Summoner not found!");

            var masteries = riotApi.ChampionMasteryV4().GetTopChampionMasteries(PlatformRoute.BR1, summoner.Id);

            return masteries;
        }

        public Summoner GetSummoner(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Summoner not found!");

            return summoner;
        }
    }
}
