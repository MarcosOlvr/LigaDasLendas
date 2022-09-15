using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.ChampionMasteryV4;
using Camille.RiotGames.LeagueV4;
using Camille.RiotGames.SummonerV4;
using League.Api.Repositories.Contracts;
using RiotSharp;

namespace League.Api.Repositories
{
    public class SummonerRepository : ISummonerRepository
    {
        RiotGamesApi riotApi;
        RiotApi ddragon;

        public SummonerRepository()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
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

        public string GetSummonerIcon(int id)
        {
            var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
            var lastestVersion = allVersion[0];
            var allIcons = ddragon.StaticData.ProfileIcons.GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR)
                .Result
                .ProfileIcons;

            var summonerIcon = allIcons.FirstOrDefault(x => x.Value.Id == id).Value.Id;

            var iconUrl = $"http://ddragon.leagueoflegends.com/cdn/12.17.1/img/profileicon/{summonerIcon}.png";

            return iconUrl;
        }

        public LeagueEntry[] GetLeagueSummoner(string summonerId)
        {
            var league = riotApi.LeagueV4().GetLeagueEntriesForSummoner(PlatformRoute.BR1, summonerId);

            return league;
        }
    }
}
