using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source= GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndPoints();

app.Run();