# Bakuchi API
This project is a backend web service for parimutuel betting, designed to be 
consumed / interacted with by a Discord client bot.

## Instructions to run
1. Clone this repository
2. Install all required dependencies by running `dotnet restore` or 
`nuget restore`
3. Build the project, and run it!

## Migrations
Migrations allow developers to update an application's database schema as 
the application grows, while ideally preserving existing data in the database.

This API's migrations are not included in this codebase; however they
can be generated with `dotnet ef migrations add <migration_name>`, and then
applied to a database using `dotnet ef database update`.