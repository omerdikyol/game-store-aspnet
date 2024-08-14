using System;
using game_store.Data;
using game_store.Dtos;
using game_store.Entities;
using game_store.Mapping;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace game_store.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";

    // private static readonly List<GameSummaryDto> games = [
    //     new (
    //         1,
    //         "Street Fighter II",
    //         "Fighting",
    //         19.99M,
    //         new DateOnly(1992, 7, 15)
    //     ),
    //     new (
    //         2,
    //         "Final Fantasy XIV",
    //         "Roleplaying",
    //         59.99M,
    //         new DateOnly(2010, 9, 30)
    //     ),
    //     new (
    //         3,
    //         "FIFA 23",
    //         "Sports",
    //         69.99M,
    //         new DateOnly(2022, 9, 27)
    //     )
    // ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

        var gamesGroup = app.MapGroup("games").WithParameterValidation();

        // GET /games [OLD]
        // gamesGroup.MapGet("/", () => games);

        // GET /games [WITH DB]
        gamesGroup.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                    .Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync() // Convert process to async
        );

        // GET /games/{id} [OLD]
        // gamesGroup.MapGet("/{id}", (int id) => {
        //     GameDto? game = games.Find(game => game.Id == id);

        //     return game is null ? Results.NotFound(): Results.Ok(game);
        // })
        // .WithName(GetGameEndpoint);

        // GET /games/{id} [WITH DB]
        gamesGroup.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? 
            Results.NotFound(): Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpoint);


        // POST /games
        gamesGroup.MapPost("/", async (CreateGameDto createGameDto, GameStoreContext dbContext) => {
            // GameDto game = new(
            //     games.Count() + 1,
            //     createGameDto.Name,
            //     createGameDto.Genre,
            //     createGameDto.Price,
            //     createGameDto.ReleaseDate
            // );

            // games.Add(game);

            Game game = createGameDto.ToEntity();
            // game.Genre = dbContext.Genres.Find(createGameDto.GenreId); // We are not returning Genre, we don't need it anymore

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // GameSummaryDto gameDto = new(
            //     game.Id,
            //     game.Name,
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleaseDate
            // );

            return Results.CreatedAtRoute(
                GetGameEndpoint, 
                new {id = game.Id}, 
                game.ToGameDetailsDto());
        });

        // PUT /games/{id} [OLD]
        // gamesGroup.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) => {
        //     var index = games.FindIndex(game => game.Id == id);

        //     if (index==-1) {
        //         // Return Not Found or Create New Content
        //         return Results.NotFound();
        //     }

        //     games[index] = new GameSummaryDto(
        //         id,
        //         updateGameDto.Name,
        //         updateGameDto.Genre,
        //         updateGameDto.Price,
        //         updateGameDto.ReleaseDate
        //     );

        //     return Results.NoContent();
        // });

        // PUT /games/{id} [WITH DB]
        gamesGroup.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) => {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame == null) {
                // Return Not Found or Create New Content
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updateGameDto.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games{id} [OLD]
        // gamesGroup.MapDelete("/{id}", (int id) => {
        //     games.RemoveAll(game => game.Id == id);

        //     return Results.NoContent();
        // });

        // DELETE /games{id} [WITH DB]
        gamesGroup.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {
            await dbContext.Games
            .Where(game => game.Id == id)
            .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return gamesGroup;
    }
}
