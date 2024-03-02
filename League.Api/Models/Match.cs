namespace League.Api.Models
{
    public class Match
    {
        public Camille.RiotGames.MatchV5.Match MatchList { get; set; }
        public List<Item> ItemList { get; set; }
    }
}
