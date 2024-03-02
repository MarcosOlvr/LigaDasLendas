using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.MatchV5;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;
using RiotSharp.Endpoints.StaticDataEndpoint.Item;
using RiotSharp.Endpoints.StaticDataEndpoint.ReforgedRune;
using Match = League.Api.Models.Match;

namespace League.Api.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        RiotGamesApi riotApi;
        RiotApi ddragon;
        string latestVersion;

        public MatchRepository()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);

            var allVersion = ddragon.DataDragon.Versions.GetAllAsync().Result;
            latestVersion = allVersion[0];
        }

        public List<Match> GetMatches(string riotId, string tagLine)
        {
            var riotAccount = riotApi.AccountV1().GetByRiotId(RegionalRoute.AMERICAS, riotId, tagLine);
            var lastMatches = riotApi.MatchV5().GetMatchIdsByPUUID(RegionalRoute.AMERICAS, riotAccount.Puuid, 10);
            var allItems = ddragon.DataDragon.Items.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Items.Values;
            var summoner = riotApi.SummonerV4().GetByPUUID(PlatformRoute.BR1, riotAccount.Puuid);
            var itemImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/item/";

            if (lastMatches == null)
                throw new Exception("Histórico de partida não encontrado!");

            var listOfMatches = new List<Match>();

            foreach(var matchId in  lastMatches)
            {
                var itemList = new List<Item>();
                var items = new List<ItemStatic>();
                var match = riotApi.MatchV5().GetMatch(RegionalRoute.AMERICAS, matchId);

                if (match == null)
                    throw new Exception("Dados da partida não encontrados! Possa ser que o Id informado não exista.");

                var player = match.Info.Participants.Where(x => x.SummonerId == summoner.Id);

                foreach (var item in player)
                {
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item0));
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item1));
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item2));
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item3));
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item4));
                    items.Add(allItems.FirstOrDefault(x => x.Id == item.Item5));

                    foreach (var i in items)
                    {
                        if (i == null)
                            continue;

                        var newItem = new Item()
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Image = itemImageUrl + i.Image.Full
                        };

                        itemList.Add(newItem);
                    }
                }

                var newMatch = new Match()
                {
                    MatchList = match,
                    ItemList = itemList
                };

                listOfMatches.Add(newMatch);
            }

            return listOfMatches;
        }
    }
}
