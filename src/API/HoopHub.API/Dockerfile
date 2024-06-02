#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/HoopHub.API/HoopHub.API.csproj", "API/HoopHub.API/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Infrastructure/HoopHub.Modules.NBAData.Infrastructure.csproj", "Modules/NBAData/HoopHub.Modules.NBAData.Infrastructure/"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Infrastructure/HoopHub.BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/HoopHub.BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Application/HoopHub.BuildingBlocks.Application.csproj", "BuildingBlocks/HoopHub.BuildingBlocks.Application/"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Domain/HoopHub.BuildingBlocks.Domain.csproj", "BuildingBlocks/HoopHub.BuildingBlocks.Domain/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Application/HoopHub.Modules.NBAData.Application.csproj", "Modules/NBAData/HoopHub.Modules.NBAData.Application/"]
COPY ["Modules/UserFeatures/HoopHub.Modules.UserFeatures.IntegrationEvents/HoopHub.Modules.UserFeatures.IntegrationEvents.csproj", "Modules/UserFeatures/HoopHub.Modules.UserFeatures.IntegrationEvents/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Domain/HoopHub.Modules.NBAData.Domain.csproj", "Modules/NBAData/HoopHub.Modules.NBAData.Domain/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.IntegrationEvents/HoopHub.Modules.NBAData.IntegrationEvents.csproj", "Modules/NBAData/HoopHub.Modules.NBAData.IntegrationEvents/"]
COPY ["Modules/UserAccess/HoopHub.Modules.UserAccess.Infrastructure/HoopHub.Modules.UserAccess.Infrastructure.csproj", "Modules/UserAccess/HoopHub.Modules.UserAccess.Infrastructure/"]
COPY ["Modules/UserAccess/HoopHub.Modules.UserAccess.Application/HoopHub.Modules.UserAccess.Application.csproj", "Modules/UserAccess/HoopHub.Modules.UserAccess.Application/"]
COPY ["Modules/UserAccess/HoopHub.Modules.UserAccess.Domain/HoopHub.Modules.UserAccess.Domain.csproj", "Modules/UserAccess/HoopHub.Modules.UserAccess.Domain/"]
COPY ["Modules/UserAccess/HoopHub.Modules.UserAccess.IntegrationEvents/HoopHub.Modules.UserAccess.IntegrationEvents.csproj", "Modules/UserAccess/HoopHub.Modules.UserAccess.IntegrationEvents/"]
COPY ["Modules/UserFeatures/HoopHub.Modules.UserFeatures.Application/HoopHub.Modules.UserFeatures.Application.csproj", "Modules/UserFeatures/HoopHub.Modules.UserFeatures.Application/"]
COPY ["Modules/UserFeatures/HoopHub.Modules.UserFeatures.Domain/HoopHub.Modules.UserFeatures.Domain.csproj", "Modules/UserFeatures/HoopHub.Modules.UserFeatures.Domain/"]
RUN dotnet restore "./API/HoopHub.API/./HoopHub.API.csproj"
COPY . .
WORKDIR "/src/API/HoopHub.API"
RUN dotnet build "./HoopHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HoopHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HoopHub.API.dll"]