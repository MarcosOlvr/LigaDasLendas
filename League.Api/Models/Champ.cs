﻿namespace League.Api.Models
{
    public class Champ
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lore { get; set; }
        public string Title { get; set; }
        public string LoadScreenImage { get; set; }
        public string SquareImage { get; set; }
        public List<string> Tags { get; set; } 
        public List<ChampSkills> Skills { get; set; }
        public List<Skin> Skins { get; set; }
    }
}
