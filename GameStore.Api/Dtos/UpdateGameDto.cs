namespace GameStore.Api.Dtos;

public record class UpdateGameDto
(
    string Name, 
    string Genre,
    Decimal Price,
    DateOnly ReleaseDate
);