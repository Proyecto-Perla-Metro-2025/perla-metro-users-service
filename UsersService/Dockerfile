# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY src/*.csproj ./src/
RUN dotnet restore src/*.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish src/*.csproj -c Release -o out

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "UsersService.dll"]
