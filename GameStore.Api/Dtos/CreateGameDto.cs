namespace GameStore.Api.Dtos;

public record class CreateGameDto
(
    string Name, 
    string Genre,
    Decimal Price,
    DateOnly ReleaseDate
);