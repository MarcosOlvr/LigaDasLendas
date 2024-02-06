using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.LeagueV4;
using Camille.RiotGames.SummonerV4;
using League.Api.Models;
using League.Api.Repositories.Contracts;
using RiotSharp;

namespace League.Api.Repositories
{
    public class SummonerRepository : ISummonerRepository
    {
        RiotGamesApi riotApi;
        RiotApi ddragon;
        IChampRepository _champRepository;
        string latestVersion;
        string runeImagePath;
        string runeIconPath;
        string spellImagePath;

        public SummonerRepository(IChampRepository champRepository)
        {
            riotApi = RiotGamesApi.NewInstance(Settings.Key);
            ddragon = RiotApi.GetDevelopmentInstance(Settings.Key);
            _champRepository = champRepository;

            var allVersion = ddragon.DataDragon.Versions.GetAllAsync().Result;
            latestVersion = allVersion[0];

            runeImagePath = "https://raw.githubusercontent.com/InFinity54/LoL_DDragon/master/img/";
            runeIconPath = "https://lolstatic-a.akamaihd.net/frontpage/apps/prod/preseason-2018/pt_BR/a6708b7ae3dbc0b25463f9c8e259a513d2c4c7e6/assets/img/customizer/";
            spellImagePath = $"http://ddragon.leagueoflegends.com/cdn/{latestVersion}/img/spell/";
        }

        public List<Rune> GetAllRunes()
        {
            var runes = ddragon.DataDragon.ReforgedRunes.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result;
            var allRunes = new List<Rune>();

            if (runes == null)
                throw new Exception("Runas não encontradas!");

            foreach (var rune in runes)
            {
                var r = new Rune();
                r.Name = rune.Name;
                r.Icon = runeIconPath + rune.Id + "/stripe.jpg";

                allRunes.Add(r);
            }

            return allRunes;
        }

        public Rune GetRuneByName(string name)
        {
            var runes = ddragon.DataDragon.ReforgedRunes.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result;

            if (runes == null)
                throw new Exception("Runas não encontradas!");


            var rune = runes.FirstOrDefault(x => x.Name == name);
            var newRune = new Rune();
            List<ReforgedRunes> slots = new List<ReforgedRunes>();

            foreach (var slot in rune.Slots)
            {
                foreach (var s in slot.Runes)
                {
                    var reforgedRune = new ReforgedRunes();

                    reforgedRune.Name = s.Name;
                    reforgedRune.Icon = runeImagePath + s.Icon;
                    reforgedRune.Description = s.ShortDescription;

                    slots.Add(reforgedRune);
                }
            }

            newRune.Name = rune.Name;
            newRune.Icon = runeImagePath + rune.Icon;
            newRune.Slots = slots;

            return newRune;
        }

        public List<Masteries> GetChampMastery(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Invocador não encontrado!");

            var masteries = riotApi.ChampionMasteryV4().GetTopChampionMasteriesByPUUID(PlatformRoute.BR1, summoner.Puuid);

            if (masteries == null)
                throw new Exception("Maestrias não foram localizadas!");

            var list = new List<Masteries>();

            foreach (var item in masteries)
            {
                var m = new Masteries();
                m.ChampionLevel = item.ChampionLevel;
                m.ChampionPoints = item.ChampionPoints;
                m.Champion = _champRepository.GetChampById(((int)item.ChampionId));

                list.Add(m);
            }

            return list;
        }

        public Summoner GetSummoner(string summonerName)
        {
            var summoner = riotApi.SummonerV4().GetBySummonerName(PlatformRoute.BR1, summonerName);
            if (summoner == null)
                throw new Exception("Invocador não encontrado!");

            return summoner;
        }

        public string GetSummonerIcon(int id)
        {
            var allVersion = ddragon.DataDragon.Versions.GetAllAsync().Result;
            var lastestVersion = allVersion[0];
            var allIcons = ddragon.DataDragon.ProfileIcons.GetAllAsync(lastestVersion, RiotSharp.Misc.Language.pt_BR)
                .Result
                .ProfileIcons;

            var summonerIcon = allIcons.FirstOrDefault(x => x.Value.Id == id).Value.Id;

            var iconUrl = $"http://ddragon.leagueoflegends.com/cdn/12.17.1/img/profileicon/{summonerIcon}.png";

            return iconUrl;
        }

        public LeagueEntry[] GetLeagueSummoner(string summonerId)
        {
            var league = riotApi.LeagueV4().GetLeagueEntriesForSummoner(PlatformRoute.BR1, summonerId);

            if (league == null)
                throw new Exception("Liga não encontrada!");

            return league;
        }

        public List<Spell> GetAllSpells()
        {
            var allSpells = ddragon.DataDragon.SummonerSpells.GetAllAsync(latestVersion, RiotSharp.Misc.Language.pt_BR).Result.SummonerSpells.Values.ToList();
            var spellsList = new List<Spell>();

            foreach (var spell in allSpells)
            {
                var newSpell = new Spell();

                newSpell.Name = spell.Name;
                newSpell.Description = spell.Description;
                newSpell.Image = spellImagePath + spell.Image.Full;
                newSpell.Cooldown = spell.CooldownBurn;

                spellsList.Add(newSpell);
            }


            return spellsList;
        }
    }
}
