# InfinionCodingChallenge_ByTaiwo

This application demonstrates best practices for implementing authentication and clean architecture in a .NET Core application.

## Prerequisites

- .NET 8.0 SDK 
- SQL Server 
- Visual Studio 2022 

## Getting Started

1. Clone the repository:
git clone https://github.com/Taiwo525/InfinionCodingChallenge_ByTaiwo.git
cd InfinionCodingChallenge_ByTaiwo

2. Update the connection string in `appsettings.json`:

Update the JWT settings in appsettings.json as well

Open a terminal in the project directory and run the following commands to set up the database:

dotnet ef migrations add InitialCreate
dotnet ef database update
Build and run the application:


dotnet build
dotnet run

Features
User registration with email confirmation
Secure login 
JWT-based authentication for API endpoints
Password complexity requirements 
CRUD operation on product with search, filtering and pagenation functionality

API Endpoints
POST /api/account/register - Register a new user
POST /api/account/login - Log in and receive a JWT
Endpoints for product CRUD operation
