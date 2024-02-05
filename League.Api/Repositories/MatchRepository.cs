using Camille.RiotGames;
using Camille.RiotGames.MatchV5;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;
using RiotSharp.Endpoints.StaticDataEndpoint.Item;
using RiotSharp.Endpoints.StaticDataEndpoint.ReforgedRune;

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

        public Item GetItem(int itemId)
        {
            var allItems = ddragon.DataDragon.Items.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Items.Values;
            var item = allItems.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new Exception("Item não encontrado!");

            var itemImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/item/";

            var i = new Item()
            {
                Id = item.Id,
                Name = item.Name,
                Image = itemImageUrl + item.Image.Full
            };

            return i;
        }

        public List<Item> GetItems(
            int item1, int item2, 
            int item3, int item4, 
            int item5, int item6)
        {
            var allItems = ddragon.DataDragon.Items.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Items.Values;
            var fItem = allItems.FirstOrDefault(x => x.Id == item1);
            var sItem = allItems.FirstOrDefault(x => x.Id == item2);
            var tItem = allItems.FirstOrDefault(x => x.Id == item3);
            var foItem = allItems.FirstOrDefault(x => x.Id == item4);
            var fiItem = allItems.FirstOrDefault(x => x.Id == item5);
            var sixItem = allItems.FirstOrDefault(x => x.Id == item6);

            var items = new List<ItemStatic>();
            items.Add(fItem);
            items.Add(sItem);
            items.Add(tItem);
            items.Add(foItem);
            items.Add(fiItem);
            items.Add(sixItem);

            if (items == null)
                throw new Exception("Os itens não foram encontrados!");
            
            var itemList = new List<Item>();
            var itemImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/item/";

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                var i = new Item()
                { 
                    Id = item.Id,
                    Name = item.Name,
                    Image = itemImageUrl + item.Image.Full
                };

                itemList.Add(i);
            }

            return itemList;
        }

        public Match GetMatchById(string id)
        {
            var match = riotApi.MatchV5().GetMatch(Camille.Enums.RegionalRoute.AMERICAS, id);

            if (match == null)
                throw new Exception("Dados da partida não encontrados! Possa ser que o Id informado não exista.");

            return match;
        }

        public List<string> GetMatches(string summonerPuuid)
        {
            var lastMatches = riotApi.MatchV5().GetMatchIdsByPUUID(Camille.Enums.RegionalRoute.AMERICAS, summonerPuuid, 10);

            if (lastMatches == null)
                throw new Exception("Histórico de partida não encontrado!");

            return lastMatches.ToList();
        }
    }
}
