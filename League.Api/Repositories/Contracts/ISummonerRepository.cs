namespace League.Api.Repositories.Contracts
{
    public interface ISummonerRepository
    {
        Camille.RiotGames.ChampionMasteryV4.ChampionMastery[] GetChampMastery(string summonerName);
        Camille.RiotGames.SummonerV4.Summoner GetSummoner(string summonerName);
    }
}
