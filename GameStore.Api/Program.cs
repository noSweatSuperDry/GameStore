using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()                
              .AllowAnyMethod();               
    });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGamesEndPoints();

app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();