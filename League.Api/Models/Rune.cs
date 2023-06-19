namespace League.Api.Models
{
    public class Rune
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<ReforgedRunes> Slots { get; set; }
    }
}
