namespace GameStore.Api.Dtos;

public record class GameDetailsDto(
    int Id,
    string Name, 
    int GenreId,
    Decimal Price,
    DateOnly ReleaseDate
);