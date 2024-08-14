using game_store.Dtos;
using game_store.Entities;

namespace game_store.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
