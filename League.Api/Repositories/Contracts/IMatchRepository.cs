using League.Api.Models;

namespace League.Api.Repositories.Contracts
{
    public interface IMatchRepository
    {
        List<Match> GetMatches(string riotId, string tagLine);
    }
}
