using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.ChampionMasteryV4;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;

namespace League.Api.Repositories
{
    public class ChampRepository : IChampRepository
    {
        RiotApi ddragon;

        public ChampRepository()
        {
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
        }

        public List<Champ> GetAllChamps()
        {
            var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
            var lastestVersion = allVersion[0];
            var allChamps = ddragon.StaticData.Champions
                .GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

            List<Champ> champions = new List<Champ>();
            var loadScreenImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
            var squareImageUrl = "http://ddragon.leagueoflegends.com/cdn/12.17.1/img/champion/";

            foreach (var champ in allChamps)
            {
                List<string> champTags = new List<string>();

                var c = new Champ()
                {
                    Id = champ.Id,
                    Name = champ.Name,
                    Lore = champ.Lore,
                    Title = champ.Title,
                    LoadScreenImage = loadScreenImageUrl + champ.Name + "_0.jpg",
                    SquareImage = squareImageUrl + champ.Image.Full
                };

                foreach (var t in champ.Tags)
                {
                    champTags.Add(t.ToString());
                }

                c.Tags = champTags;
                champions.Add(c);
            }

            return champions;
        }

        public Champ GetChampById(int id)
        {
            var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
            var lastestVersion = allVersion[0];
            var allChamps = ddragon.StaticData.Champions.GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

            var championById = allChamps.FirstOrDefault(x => x.Id == id);

            if (championById == null)
                throw new Exception("Champion not found!");

            var loadScreenImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
            var squareImageUrl = "http://ddragon.leagueoflegends.com/cdn/12.17.1/img/champion/";

            var champTags = new List<string>();

            foreach (var t in championById.Tags)
            {
                champTags.Add(t.ToString());
            }

            var champ = new Champ()
            {
                Id = championById.Id,
                Name = championById.Name,
                Lore = championById.Lore,
                Title = championById.Title,
                LoadScreenImage = loadScreenImageUrl + championById.Name + "_0.jpg",
                SquareImage = squareImageUrl + championById.Image.Full,
                Tags = champTags
            };

            return champ;
        }
    }
}
