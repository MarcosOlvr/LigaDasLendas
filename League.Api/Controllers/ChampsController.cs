using Microsoft.AspNetCore.Mvc;
using Camille.Enums;
using Camille.RiotGames;
using RiotSharp;
using System.Collections.Generic;
using League.Api.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
    public class ChampsController : ControllerBase
    {
        private readonly IChampRepository _champRepo;

        public ChampsController(IChampRepository champRepo)
        {
            _champRepo = champRepo;
        }

        [HttpGet("champ/{skip:int}/{take:int}")]
        public ActionResult GetAllChamps(int skip, int take)
        {
            try
            {
                var allChamps = _champRepo.GetAllChamps();

                var champs = allChamps.Skip(skip).Take(take);

                var total = allChamps.Count();

                return Ok(new {
                    total, 
                    data = champs
                });
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
                var champ = _champRepo.GetChampById(id);

                if (champ == null)
                    return NotFound();

                return Ok(champ);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("champ/{champName}")]
        public ActionResult GetChampByName(string champName)
        {
            try
            {
                var champ = _champRepo.GetChampByName(champName);

                if (champ == null)
                    return NotFound();

                return Ok(champ);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
