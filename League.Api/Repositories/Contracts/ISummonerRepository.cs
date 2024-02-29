using League.Api.Models;

namespace League.Api.Repositories.Contracts
{
    public interface ISummonerRepository
    {
        List<Masteries> GetChampMastery(string summonerName, string tagLine);
        Camille.RiotGames.SummonerV4.Summoner GetSummoner(string summonerName, string tagName);
        Camille.RiotGames.LeagueV4.LeagueEntry[] GetLeagueSummoner(string summonerName, string tagLine);
        string GetSummonerIcon(int id);
        string GetSummonerIcon(string summonerName, string tagLine);
        List<Rune> GetAllRunes();
        Rune GetRuneByName(string name);
        List<Spell> GetAllSpells();
    }
}
