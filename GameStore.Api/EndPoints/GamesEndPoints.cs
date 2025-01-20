using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();

        const string GetGameEndPointName = "Getgame";

        //GET games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
                        .Include(game => game.Genre)
                        .Select(game => game.ToGameSummaryDto())
                        .AsNoTracking()
                        .ToListAsync()
                        );

        //GET a game by id

        group.MapGet("/{id}",async (int id, GameStoreContext dbConstext) =>
         {
             Game? game = await dbConstext.Games.FindAsync(id);

             return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

         }).WithName(GetGameEndPointName);



        //POST
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();


            return Results.CreatedAtRoute(
                GetGameEndPointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });


        //UPDATE PUT

        group.MapPut("/{id}",async (int id, UpdateGameDto updatedGame, GameStoreContext dbConstext) =>
        {
            var existingGame =await dbConstext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbConstext.Entry(existingGame)
            .CurrentValues
            .SetValues(updatedGame.ToEntity(id));

            await dbConstext.SaveChangesAsync();

            return Results.NoContent();
        });


        //DELETE

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
         await   dbContext.Games
            .Where(game=> game.Id == id)
            .ExecuteDeleteAsync();
            
            return Results.NoContent();
        }
        );

        return group;

    }

}
