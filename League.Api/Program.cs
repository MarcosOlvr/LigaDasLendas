using League.Api.Repositories;
using League.Api.Repositories.Contracts;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7251",
                                              "http://localhost:5173");
                      });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IChampRepository, ChampRepository>();
builder.Services.AddScoped<ISummonerRepository, SummonerRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
