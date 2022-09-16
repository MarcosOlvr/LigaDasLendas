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
            var lastMatches = _matchRepo.GetMatches(puuid);

            if (lastMatches == null) 
                return NotFound();

            return Ok(lastMatches);
        }

        [HttpGet("item/{id:int}")]
        public ActionResult GetItemById(int id)
        {
            var item = _matchRepo.GetItem(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
        
        [HttpGet("items")]
        public ActionResult GetItemsByIds([FromQuery] int item1, int item2, int item3, int item4, int item5, int item6)
        {
            var items = _matchRepo.GetItems(item1, item2, item3, item4, item5, item6);

            if (items == null)
                return NotFound();

            return Ok(items);
        }
    }
}
