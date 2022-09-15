using League.Api.Repositories;
using League.Api.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IChampRepository, ChampRepository>();
builder.Services.AddScoped<ISummonerRepository, SummonerRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
