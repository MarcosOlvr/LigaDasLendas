using Microsoft.AspNetCore.Mvc;
using League.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Memory;
using League.Api.Models;

namespace League.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [EnableCors]
    public class ChampsController : ControllerBase
    {
        private readonly IChampRepository _champRepo;
        private readonly IMemoryCache _memoryCache;

        public ChampsController(IChampRepository champRepo, IMemoryCache memoryCache)
        {
            _champRepo = champRepo;
            _memoryCache = memoryCache;
        }

        [HttpGet("champ/{skip:int}/{take:int}")]
        public ActionResult GetAllChamps(int skip, int take)
        {
            try
            {
                if (!_memoryCache.TryGetValue("allChamps", out List<Champ> allChamps))
                {
                    allChamps = _champRepo.GetAllChamps();
                    _memoryCache.Set("allChamps", allChamps, DateTimeOffset.UtcNow.AddHours(12));
                }

                var champs = allChamps.Skip(skip).Take(take);

                var total = allChamps.Count();

                return Ok(new
                {
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
                if (!_memoryCache.TryGetValue($"champ-{id}", out Champ champ))
                {
                    champ = _champRepo.GetChampById(id);
                    _memoryCache.Set($"champ-{id}", champ, DateTimeOffset.UtcNow.AddHours(12));

                    if (champ == null)
                        return NotFound();
                }

                return Ok(champ);
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

        [HttpGet("champ/{champName}")]
        public ActionResult GetChampByName(string champName)
        {
            try
            {
                if (!_memoryCache.TryGetValue($"champ-{champName}", out Champ champ))
                {
                    champ = _champRepo.GetChampByName(champName);
                    _memoryCache.Set($"champ-{champName}", champ, DateTimeOffset.UtcNow.AddHours(12));

                    if (champ == null)
                        return NotFound();
                }

                return Ok(champ);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = ex.Message
                });
            }
        }
    }
}
