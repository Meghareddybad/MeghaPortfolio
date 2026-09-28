# Stage 1: Build & Publish Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["MeghaPortfolio.API.csproj", "./"]
RUN dotnet restore "MeghaPortfolio.API.csproj"

# Copy source code and build production release
COPY . .
RUN dotnet publish "MeghaPortfolio.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Lightweight Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose HTTP port 8080 (standard Docker non-root port)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "MeghaPortfolio.API.dll"]
