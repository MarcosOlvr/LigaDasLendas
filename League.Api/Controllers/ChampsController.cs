using Microsoft.AspNetCore.Mvc;
using Camille.Enums;
using Camille.RiotGames;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ChampsController : ControllerBase
    {
        RiotGamesApi riotApi;

        public ChampsController()
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
        }

        [HttpGet("masteries/{summonerName}")]
        public ActionResult GetMasteriesChamps([FromRoute] string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                return NotFound();

            var masteries = riotApi.ChampionMasteryV4().GetTopChampionMasteries(PlatformRoute.BR1, summoner.Id);
            
            foreach (var champ in masteries)
            {
                Console.WriteLine(champ.ChampionId.ToString().ToUpperInvariant());
            }

            return Ok(masteries);
        }

        [HttpGet]
        public ActionResult GetChamp(int id)
        {
            var champ = riotApi.ChampionV3().GetChampionInfo(PlatformRoute.BR1);

            return Ok(champ);
        }
    }
}
