# Use the official .NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["UsersService.csproj", "./"]
RUN dotnet restore "UsersService.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "UsersService.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "UsersService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a non-privileged user that the app will run under
RUN addgroup --group appgroup && adduser --disabled-password --ingroup appgroup appuser
USER appuser

ENTRYPOINT ["dotnet", "UsersService.dll"]