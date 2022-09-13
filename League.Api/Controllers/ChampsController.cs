using Microsoft.AspNetCore.Mvc;
using Camille.Enums;
using Camille.RiotGames;
using RiotSharp;
using System.Collections.Generic;
using League.Api.Models;
using Microsoft.AspNetCore.Mvc.Routing;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ChampsController : ControllerBase
    {
        RiotGamesApi riotApi;
        RiotApi ddragon;

        public ChampsController()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
        }

        [HttpGet("masteries/{summonerName}")]
        public ActionResult GetMasteriesChamps([FromRoute] string summonerName)
        {
            try
            {
                var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
                if (summoner == null)
                    return NotFound();

                var masteries = riotApi.ChampionMasteryV4().GetTopChampionMasteries(PlatformRoute.BR1, summoner.Id);

                return Ok(masteries);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }

        [HttpGet("champ/all")]
        public ActionResult GetAllChamps()
        {
            try
            {
                var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
                var lastestVersion = allVersion[0];
                var allChamps = ddragon.StaticData.Champions.GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

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

                return Ok(champions);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
            
        }

        [HttpGet("champ/{id:int}")]
        public ActionResult GetChampById(int id)
        {
            try
            {
                var allVersion = ddragon.StaticData.Versions.GetAllAsync().Result;
                var lastestVersion = allVersion[0];
                var allChamps = ddragon.StaticData.Champions.GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR).Result.Champions.Values;

                var championById = allChamps.FirstOrDefault(x => x.Id == id);

                if (championById == null)
                    return NotFound();

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

                return Ok(champ);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }
    }
}
