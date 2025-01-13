namespace GameStore.Api.Dtos;

public record class GameDto(
    int Id,
    string Name, 
    string Genre,
    Decimal Price,
    DateOnly ReleaseDate
);