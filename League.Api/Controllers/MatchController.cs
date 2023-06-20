using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
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
            try
            {
                var match = _matchRepo.GetMatchById(id);

                return Ok(match);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("match/latest/{puuid}/")]
        public ActionResult GetLastMatches(string puuid)
        {
            try
            {
                var lastMatches = _matchRepo.GetMatches(puuid);

                return Ok(lastMatches);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("item/{id:int}")]
        public ActionResult GetItemById(int id)
        {
            try
            {
                var item = _matchRepo.GetItem(id);

                return Ok(item);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }
        
        [HttpGet("items")]
        public ActionResult GetItemsByIds([FromQuery] int item1, int item2, int item3, int item4, int item5, int item6)
        {
            try
            {
                var items = _matchRepo.GetItems(item1, item2, item3, item4, item5, item6);

                return Ok(items);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }
    }
}
