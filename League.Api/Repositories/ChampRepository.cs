﻿using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;
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
        string skinImageUrl;

        public ChampRepository()
        {
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);

            var allVersion = ddragon.DataDragon.Versions.GetAllAsync().Result;
            latestVersion = allVersion[0];

            loadScreenImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
            squareImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/champion/";
            skillImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/spell/";
            passiveImageUrl = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/passive/";
            skinImageUrl = $"http://ddragon.leagueoflegends.com/cdn/img/champion/splash/";
        }

        public Champ CreateChamp(ChampionStatic champ)
        {
            List<string> champTags = new List<string>();
            List<ChampSkills> skills = new List<ChampSkills>();
            List<Skin> skins = new List<Skin>();

            var champNameSplash = champ.Image.Full.Replace(".png", String.Empty);

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

            foreach (var skin in champ.Skins)
            {
                var newSkin = new Skin()
                {
                    Name = skin.Name,
                    SkinImage = skinImageUrl + $"{champNameSplash}_{skin.Num}.jpg"
                };

                skins.Add(newSkin);
            }

            var c = new Champ()
            {
                Id = champ.Id,
                Name = champ.Name,
                Lore = champ.Lore,
                Title = champ.Title,
                LoadScreenImage = loadScreenImageUrl + champNameSplash + "_0.jpg",
                SquareImage = squareImageUrl + champ.Image.Full,
                Tags = champTags,
                Skills = skills,
                Skins = skins
            };

            return c;
        }

        public List<Champ> GetAllChamps()
        {
            var allChamps = ddragon.DataDragon.Champions
                .GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

            List<Champ> champions = new List<Champ>();

            if (champions == null)
                throw new Exception("Os campeões não foram encontrados!");

            foreach (var champ in allChamps)
            {
                var c = CreateChamp(champ);

                champions.Add(c);
            }

            return champions;
        }

        public Champ GetChampById(int id)
        {
            var allChamps = ddragon.DataDragon.Champions
                .GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;
            var championById = allChamps.FirstOrDefault(x => x.Id == id);
            
            if (championById == null)
                throw new Exception("Campeão não encontrado! Talvez possa ser um erro no ID =)");

            var champ = CreateChamp(championById);

            return champ;
        }

        public Champ GetChampByName(string name)
        {
            var allChamps = ddragon.DataDragon.Champions
                .GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

            var championByName = allChamps.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (championByName == null)
                throw new Exception("Campeão não encontrado! Talvez possa ser um erro de digitação =)");

            var champ = CreateChamp(championByName);

            return champ;
        }
    }
}
