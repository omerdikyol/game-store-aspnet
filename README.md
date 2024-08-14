# Game Store API

## Project Overview

The Game Store API is a simple web API built to manage a collection of video games and their associated genres. It allows users to retrieve information about games and genres, create new entries, update existing entries, and delete them as needed. The API is designed using the ASP.NET Core framework and leverages SQLite as its database for data persistence.

## Technologies Used

- **ASP.NET Core**: A cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications. In this project, it's used to build the web API that handles HTTP requests and serves responses.
- **Entity Framework Core**: An Object-Relational Mapper (ORM) for .NET, enabling developers to work with a database using .NET objects. It eliminates the need for most of the data-access code developers typically need to write.
- **SQLite**: A lightweight, disk-based database that doesn’t require a separate server process. It is used in this project for storing and managing data.
- **C#**: The primary programming language used for developing this API.
- **Visual Studio 2022**: The integrated development environment (IDE) used for developing the project.

## Project Structure

- **game-store.api.csproj**: The project file that defines the SDK, dependencies, and configuration for building the API.
- **Program.cs**: The entry point of the application where services are configured and the application is built and run. The database is migrated, and HTTP endpoints for games and genres are mapped here.
- **GameStoreContext.cs**: Defines the database context for the application, including the DbSet properties for Games and Genres and the model configurations.
- **DataExtensions.cs**: Contains an extension method for migrating the database during application startup.

## Getting Started

### Prerequisites

- .NET SDK 6.0 or higher
- Visual Studio 2022 or any other compatible IDE
- SQLite

### Running the Application

1. **Clone the repository** to your local machine.
2. Open the solution (`game-store.sln`) in Visual Studio.
3. Restore the necessary packages by building the solution.
4. Update the connection string in `Program.cs` if needed.
5. Run the application using the built-in Visual Studio IIS Express or via the command line with `dotnet run`.

The API will be available at `https://localhost:<port>` by default.

### API Endpoints

- **GET /games**: Retrieve a list of all games.
- **GET /genres**: Retrieve a list of all genres.
- **POST /games**: Add a new game.
- **POST /genres**: Add a new genre.
- **PUT /games/{id}**: Update an existing game.
- **PUT /genres/{id}**: Update an existing genre.
- **DELETE /games/{id}**: Delete an existing game.
- **DELETE /genres/{id}**: Delete an existing genre.
