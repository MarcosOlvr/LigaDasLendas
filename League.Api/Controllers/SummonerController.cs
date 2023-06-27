﻿using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
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
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
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
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
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
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
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
                var runes = _summonerRepo.GetAllRunes();

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
                var rune = _summonerRepo.GetRuneByName(runeName);

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
                var spells = _summonerRepo.GetAllSpells();

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
