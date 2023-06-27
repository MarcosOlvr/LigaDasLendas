using League.Api.Models;

namespace League.Api.Repositories.Contracts
{
    public interface ISummonerRepository
    {
        List<Masteries> GetChampMastery(string summonerName);
        Camille.RiotGames.SummonerV4.Summoner GetSummoner(string summonerName);
        Camille.RiotGames.LeagueV4.LeagueEntry[] GetLeagueSummoner(string summonerName);
        string GetSummonerIcon(int id);
        List<Rune> GetAllRunes();
        Rune GetRuneByName(string name);
        List<Spell> GetAllSpells();
    }
}
