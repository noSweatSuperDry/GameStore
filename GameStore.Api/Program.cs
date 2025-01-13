using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new GameDto(
        Id: 1,
        Name: "Path of Exile",
        Genre: "Action RPG",
        Price: 0.00m,
        ReleaseDate: new DateOnly(2013, 10, 23)
    ),
    new GameDto(
        Id: 2,
        Name: "Diablo 4",
        Genre: "Action RPG",
        Price: 69.99m,
        ReleaseDate: new DateOnly(2023, 6, 6)
    ),
    new GameDto(
        Id: 3,
        Name: "League of Legends",
        Genre: "MOBA",
        Price: 0.00m,
        ReleaseDate: new DateOnly(2009, 10, 27)
    ),
    new GameDto(
        Id: 4,
        Name: "Call of Duty: Modern Warfare",
        Genre: "First-Person Shooter",
        Price: 59.99m,
        ReleaseDate: new DateOnly(2019, 10, 25)
    )
];

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();