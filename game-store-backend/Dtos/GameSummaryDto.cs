namespace game_store.Dtos;

public record class GameSummaryDto(
    int Id, 
    string Name, 
    string Genre, 
    decimal Price,
    DateOnly ReleaseDate
);