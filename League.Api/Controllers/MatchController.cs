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

        [HttpGet("match/latest/{riotId}-{tagLine}/")]
        public ActionResult GetLastMatches(string riotId, string tagLine)
        {
            try
            {
                var lastMatches = _matchRepo.GetMatches(riotId, tagLine);

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
    }
}
