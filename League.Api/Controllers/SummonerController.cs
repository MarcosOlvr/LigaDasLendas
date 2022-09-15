using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class SummonerController : ControllerBase
    {
        private readonly ISummonerRepository _summonerRepo;

        public SummonerController(ISummonerRepository summonerRepository)
        {
            _summonerRepo = summonerRepository;
        }

        [HttpGet("summoner/{summonerName}")]
        public ActionResult GetSummonerByName([FromRoute] string summonerName)
        {
            try
            {
                var summoner = _summonerRepo.GetSummoner(summonerName);

                return Ok(summoner);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }

        [HttpGet("masteries/{summonerName}")]
        public ActionResult GetMasteriesChamps([FromRoute] string summonerName)
        {
            try
            {
                var masteries = _summonerRepo.GetChampMastery(summonerName);

                return Ok(masteries);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }

        [HttpGet("icon/{id:int}")]
        public ActionResult GetProfileIconById([FromRoute] int id)
        {
            try
            {
                var profileIconUrl = _summonerRepo.GetSummonerIcon(id);

                return Ok(profileIconUrl);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }
        
        [HttpGet("league/{summonerId}")]
        public ActionResult GetLeague([FromRoute] string summonerId)
        {
            try
            {
                var league = _summonerRepo.GetLeagueSummoner(summonerId);

                return Ok(league);
            }
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }
    }
}
