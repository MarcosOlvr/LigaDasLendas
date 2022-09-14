using Microsoft.AspNetCore.Mvc;
using Camille.Enums;
using Camille.RiotGames;
using RiotSharp;
using System.Collections.Generic;
using League.Api.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using League.Api.Repositories.Contracts;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ChampsController : ControllerBase
    {
        private readonly IChampRepository _champRepo;

        public ChampsController(IChampRepository champRepo)
        {
            _champRepo = champRepo;
        }

        [HttpGet("champ/all")]
        public ActionResult GetAllChamps()
        {
            try
            {
                var allChamps = _champRepo.GetAllChamps();

                return Ok(allChamps);
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
            catch
            {
                throw new Exception("Ocorreu algum problema com a API, tente novamente mais tarde!");
            }
        }
    }
}
