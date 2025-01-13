using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPointName = "Getgame";

List<GameDto> games = [
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
//GET games
app.MapGet("games", () => games);

//GET a game by id

app.MapGet("games/{id}", (int id) =>
 {
    GameDto? game = games.Find(game=> game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
    
    }).WithName(GetGameEndPointName); 

//POST

app.MapPost("games", (CreateGameDto newGame)=> 
{
    GameDto game = new(
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndPointName,new {id = game.Id}, game);
}
);


//UPDATE PUT

app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame)=>{
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

app.MapDelete("games/{id}", (int id) => 
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
}
);


app.Run();