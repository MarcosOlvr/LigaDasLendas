using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.ChampionMasteryV4;
using Camille.RiotGames.ValMatchV1;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;
using RiotSharp.Endpoints.ChampionEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;

namespace League.Api.Repositories
{
    public class ChampRepository : IChampRepository
    {
        RiotApi ddragon;
        string loadScreenImageUrl;
        string squareImageUrl;
        string skillImageUrl;
        string passiveImageUrl;
        string latestVersion;

        public ChampRepository()
        {
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
            loadScreenImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
            squareImageUrl = "http://ddragon.leagueoflegends.com/cdn/12.17.1/img/champion/";
            skillImageUrl = "http://ddragon.leagueoflegends.com/cdn/12.17.1/img/spell/";
            passiveImageUrl = "http://ddragon.leagueoflegends.com/cdn/12.17.1/img/passive/";

            var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
            latestVersion = allVersion[0];
        }

        public Champ CreateChamp(ChampionStatic champ)
        {
            List<string> champTags = new List<string>();
            List<ChampSkills> skills = new List<ChampSkills>();

            foreach (var t in champ.Tags)
            {
                champTags.Add(t.ToString());
            }

            skills.Add(new ChampSkills
                {
                    Name = champ.Passive.Name,
                    Description = champ.Passive.Description,
                    Image = passiveImageUrl + champ.Passive.Image.Full
                }
            );

            foreach (var s in champ.Spells)
            {
                var skill = new ChampSkills()
                {
                    Name = s.Name,
                    Description = s.Description,
                    Image = skillImageUrl + s.Image.Full
                };

                skills.Add(skill);
            }

            var c = new Champ()
            {
                Id = champ.Id,
                Name = champ.Name,
                Lore = champ.Lore,
                Title = champ.Title,
                LoadScreenImage = loadScreenImageUrl + champ.Name + "_0.jpg",
                SquareImage = squareImageUrl + champ.Image.Full,
                Tags = champTags,
                Skills = skills
            };

            return c;
        }

        public List<Champ> GetAllChamps()
        {
            var allChamps = ddragon.StaticData.Champions
                .GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

            List<Champ> champions = new List<Champ>();

            foreach (var champ in allChamps)
            {
                var c = CreateChamp(champ);

                champions.Add(c);
            }

            return champions;
        }

        public Champ GetChampById(int id)
        {
            var allChamps = ddragon.StaticData.Champions
                .GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;
            var championById = allChamps.FirstOrDefault(x => x.Id == id);

            var champ = CreateChamp(championById);

            return champ;
        }
    }
}
