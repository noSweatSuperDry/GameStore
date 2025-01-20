using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{

    private static readonly List<GameSummaryDto> games = [
        new (
        1,
        "Path of Exile",
        "Action RPG",
        0.00m,
        new DateOnly(2013, 10, 23)
    ),
    new (
        2,
        "Diablo 4",
        "Action RPG",
        69.99m,
        new DateOnly(2023, 6, 6)
    ),
    new (
        3,
        "League of Legends",
        "MOBA",
        0.00m,
        new DateOnly(2009, 10, 27)
    ),
    new (
        4,
        "Call of Duty: Modern Warfare",
        "First-Person Shooter",
        59.99m,
        new DateOnly(2019, 10, 25)
    )
    ];
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();

        const string GetGameEndPointName = "Getgame";

        //GET games
        group.MapGet("/", () => games);

        //GET a game by id

        group.MapGet("/{id}", (int id, GameStoreContext dbConstext) =>
         {
             Game? game = dbConstext.Games.Find(id);

             return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

         }).WithName(GetGameEndPointName);



        //POST
        int lastUsedId = games.Count > 0 ? games.Max(g => g.Id) : 0;
        group.MapPost("", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();


            return Results.CreatedAtRoute(
                GetGameEndPointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });


        //UPDATE PUT

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbConstext) =>
        {
            var existingGame = dbConstext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbConstext.Entry(existingGame)
            .CurrentValues
            .SetValues(updatedGame.ToEntity(id));

            dbConstext.SaveChanges();

            return Results.NoContent();
        });


        //DELETE

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        }
        );

        return group;

    }

}
