using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{

private static readonly List<GameDto> games = [
    new GameDto(
        1,
        "Path of Exile",
        "Action RPG",
        0.00m,
        new DateOnly(2013, 10, 23)
    ),
    new GameDto(
        2,
        "Diablo 4",
        "Action RPG",
        69.99m,
        new DateOnly(2023, 6, 6)
    ),
    new GameDto(
        3,
        "League of Legends",
        "MOBA",
        0.00m,
        new DateOnly(2009, 10, 27)
    ),
    new GameDto(
        4,
        "Call of Duty: Modern Warfare",
        "First-Person Shooter",
        59.99m,
        new DateOnly(2019, 10, 25)
    )
];
public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app){

var group = app.MapGroup("games").WithParameterValidation();

const string GetGameEndPointName = "Getgame";

//GET games
group.MapGet("/", () => games);

//GET a game by id

group.MapGet("/{id}", (int id) =>
 {
    GameDto? game = games.Find(game=> game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
    
    }).WithName(GetGameEndPointName); 



//POST
int lastUsedId = games.Count > 0 ? games.Max(g => g.Id) : 0;
group.MapPost("", (CreateGameDto newGame, GameStoreContext dbContext)=> 
{
    Game game = new(){
        Name = newGame.Name,
        Genre = dbContext.Genres.Find(newGame.GenreId),
        GenreId = newGame.GenreId,
        Price = newGame.Price,
        ReleaseDate = newGame.ReleaseDate
    };
    dbContext.Games.Add(game);
    dbContext.SaveChanges();
    return Results.CreatedAtRoute(GetGameEndPointName,new {id = game.Id}, game);
});


//UPDATE PUT

group.MapPut("/{id}", (int id, UpdateGameDto updatedGame)=>{
    var index =  games.FindIndex(game=> game.Id == id);
    if(index == -1 ){
        return Results.NotFound();
    }


    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

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
