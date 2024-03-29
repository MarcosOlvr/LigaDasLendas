﻿using League.Api.Models;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;

namespace League.Api.Repositories.Contracts
{
    public interface IChampRepository
    {
        List<Champ> GetAllChamps();
        Champ GetChampById(int id);
        Champ GetChampByName(string name);
        Champ CreateChamp(ChampionStatic champ);
    }
}
