using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepo;

        public MatchController(IMatchRepository matchRepo)
        {
            _matchRepo = matchRepo;
        }

        [HttpGet("match/{id}")]
        public ActionResult GetMatch(string id)
        {
            var match = _matchRepo.GetMatchById(id);

            if (match == null)
                return NotFound();

            return Ok(match);
        }

        [HttpGet("match/latest/{puuid}/")]
        public ActionResult GetLastMatches(string puuid)
        {
            var lastMatches = _matchRepo.GetMatchs(puuid);

            if (lastMatches == null) 
                return NotFound();

            return Ok(lastMatches);
        }
    }
}
