# Pokémon Master API

## Overview
The Pokémon Master API is a .NET Core application that allows users to interact with Pokémon data. It provides endpoints for retrieving random Pokémon, capturing Pokémon, and managing Pokémon trainers.

## Features
- Get 10 random Pokémon
- Get a specific Pokémon by ID
- Register Pokémon trainers with basic information (name, age, CPF)
- Capture Pokémon and list captured Pokémon
- Each Pokémon includes basic data, evolutions, and a base64 encoded sprite

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQLite
- Dependency Injection

## Project Structure
```
PokemonMaster
├── src
│   ├── PokemonMaster.API          # API layer
│   ├── PokemonMaster.Application    # Application logic
│   ├── PokemonMaster.Domain         # Domain entities and interfaces
│   └── PokemonMaster.Infrastructure  # Data access and external services
├── tests                            # Unit tests for various layers
└── PokemonMaster.sln                # Solution file
```

## Setup Instructions
1. Clone the repository:
   ```
   git clone <repository-url>
   cd PokemonMaster
   ```

2. Restore the dependencies:
   ```
   dotnet restore
   ```

3. Update the connection string in `src/PokemonMaster.API/appsettings.json` to point to your SQLite database.

4. Run the migrations to set up the database:
   ```
   dotnet ef database update --project src/PokemonMaster.Infrastructure
   ```

5. Start the application:
   ```
   dotnet run --project src/PokemonMaster.API
   ```

## Usage
- **Get 10 Random Pokémon**: `GET /api/pokemon/random`
- **Get Pokémon by ID**: `GET /api/pokemon/{id}`
- **Capture a Pokémon**: `POST /api/pokemon/capture`
- **List Captured Pokémon**: `GET /api/pokemon/captured`
- **Register a Trainer**: `POST /api/trainer/register`

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for details.