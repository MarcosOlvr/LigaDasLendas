using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.ChampionMasteryV4;
using Camille.RiotGames.LeagueV4;
using Camille.RiotGames.SummonerV4;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;

namespace League.Api.Repositories
{
    public class SummonerRepository : ISummonerRepository
    {
        RiotGamesApi riotApi;
        RiotApi ddragon;
        IChampRepository _champRepository;

        public SummonerRepository(IChampRepository champRepository)
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
            _champRepository = champRepository;
        }

        public List<Masteries> GetChampMastery(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Invocador não encontrado!");

            var masteries = riotApi.ChampionMasteryV4().GetTopChampionMasteries(PlatformRoute.BR1, summoner.Id);

            if (masteries == null)
                throw new Exception("Maestrias não foram localizadas!");

            var list = new List<Masteries>();

            foreach (var obj in masteries)
            {
                var m = new Masteries();
                m.ChampionLevel = obj.ChampionLevel;
                m.ChampionPoints = obj.ChampionPoints;
                m.Champion = _champRepository.GetChampById(int.Parse(obj.ChampionId.ToString()));

                list.Add(m);
            }

            return list;
        }

        public Summoner GetSummoner(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Invocador não encontrado!");

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

            if (league == null)
                throw new Exception("Liga não encontrada!");

            return league;
        }
    }
}
