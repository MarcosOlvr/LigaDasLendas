using League.Api.Models;
using RiotSharp.Endpoints.StaticDataEndpoint.SummonerSpell;

namespace League.Api.Repositories.Contracts
{
    public interface ISummonerRepository
    {
        List<Masteries> GetChampMastery(string summonerName);
        Camille.RiotGames.SummonerV4.Summoner GetSummoner(string summonerName);
        Camille.RiotGames.LeagueV4.LeagueEntry[] GetLeagueSummoner(string summonerName);
        string GetSummonerIcon(int id);
        List<Rune> GetAllRunes();
        List<SummonerSpellStatic> GetAllSpells();
    }
}
