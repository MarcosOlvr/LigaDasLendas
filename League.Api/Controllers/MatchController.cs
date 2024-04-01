using League.Api.Models;
using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository _matchRepo;
        private readonly IMemoryCache _memoryCache;

        public MatchController(IMatchRepository matchRepo, IMemoryCache memorycache)
        {
            _matchRepo = matchRepo;
            _memoryCache = memorycache;
        }

        [HttpGet("match/latest/{riotId}-{tagLine}/")]
        public ActionResult GetLastMatches(string riotId, string tagLine)
        {
            try
            {
                if (!_memoryCache.TryGetValue("matches", out List<Match> lastMatches))
                {
                    lastMatches = _matchRepo.GetMatches(riotId, tagLine);
                    _memoryCache.Set("matches", lastMatches, DateTimeOffset.UtcNow.AddMinutes(5));
                }

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
