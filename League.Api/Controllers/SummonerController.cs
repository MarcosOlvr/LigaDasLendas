using League.Api.Models;
using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RiotSharp.Endpoints.SummonerEndpoint;
using System.Diagnostics;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
    public class SummonerController : ControllerBase
    {
        private readonly ISummonerRepository _summonerRepo;
        private readonly IMemoryCache _memorycache;

        public SummonerController(ISummonerRepository summonerRepository, IMemoryCache memoryCache)
        {
            _summonerRepo = summonerRepository;
            _memorycache = memoryCache;
        }

        [HttpGet("summoner/{summonerName}-{tagLine}")]
        public ActionResult GetSummonerByName([FromRoute] string summonerName, string tagLine)
        {
            try
            {
                if (!_memorycache.TryGetValue($"summoner-{summonerName}-{tagLine}", out Camille.RiotGames.SummonerV4.Summoner summoner))
                {
                    summoner = _summonerRepo.GetSummoner(summonerName, tagLine);
                    _memorycache.Set($"summoner-{summonerName}-{tagLine}", summoner, DateTimeOffset.UtcNow.AddHours(1));
                }

                return Ok(summoner);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }

        [HttpGet("masteries/{summonerName}-{tagLine}")]
        public ActionResult GetMasteriesChamps([FromRoute] string summonerName, string tagLine)
        {
            try
            {
                if (!_memorycache.TryGetValue($"masteries-{summonerName}-{tagLine}", out List<Masteries> masteries))
                {
                    masteries = _summonerRepo.GetChampMastery(summonerName, tagLine);
                    _memorycache.Set($"masteries-{summonerName}-{tagLine}", masteries, DateTimeOffset.UtcNow.AddMinutes(10));
                }
                
                return Ok(masteries);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }
        
        [HttpGet("icon/{summonerName}-{tagLine}")]
        public ActionResult GetProfileIconBySummonerName([FromRoute] string summonerName, string tagLine)
        {
            try
            {
                if (!_memorycache.TryGetValue($"icon-{summonerName}-{tagLine}", out string? profileIconUrl))
                {
                    profileIconUrl = _summonerRepo.GetSummonerIcon(summonerName, tagLine);
                    _memorycache.Set($"icon-{summonerName}-{tagLine}", profileIconUrl, DateTimeOffset.UtcNow.AddMinutes(5));
                }

                return Ok(profileIconUrl);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }
        
        [HttpGet("league/{summonerName}-{tagLine}")]
        public ActionResult GetLeague([FromRoute] string summonerName, string tagLine)
        {
            try
            {
                if (!_memorycache.TryGetValue($"league-{summonerName}-{tagLine}", out Camille.RiotGames.LeagueV4.LeagueEntry[] league))
                {
                    league = _summonerRepo.GetLeagueSummoner(summonerName, tagLine);
                    _memorycache.Set($"league-{summonerName}-{tagLine}", league, DateTimeOffset.UtcNow.AddMinutes(10));
                }
                
                return Ok(league);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }

        [HttpGet("runes")]
        public ActionResult GetRunes()
        {
            try
            {
                if (!_memorycache.TryGetValue("runes", out List<Rune> runes))
                {
                    runes = _summonerRepo.GetAllRunes();
                    _memorycache.Set("runes", runes, DateTimeOffset.UtcNow.AddHours(12));
                }

                return Ok(runes);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 500,
                    message = ex.Message
                });

            }
        }

        [HttpGet("runes/{runeName}")]
        public ActionResult GetRuneByName([FromRoute]string runeName)
        {
            try
            {
                if (!_memorycache.TryGetValue($"rune-{runeName}", out Rune rune))
                {
                    rune = _summonerRepo.GetRuneByName(runeName);
                    _memorycache.Set($"rune-{runeName}", rune, DateTimeOffset.UtcNow.AddHours(12));
                }

                return Ok(rune);    
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

        [HttpGet("spells")]
        public ActionResult GetSpells() 
        {
            try
            {
                if (!_memorycache.TryGetValue("spells", out List<Spell> spells))
                {
                    spells = _summonerRepo.GetAllSpells();
                    _memorycache.Set("spells", spells, DateTimeOffset.UtcNow.AddHours(12));
                }

                return Ok(spells);
            }
            catch (Exception ex)
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
