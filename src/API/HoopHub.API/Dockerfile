#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["API/HoopHub.API/HoopHub.API.csproj", "API/HoopHub.API/"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.API/HoopHub.BuildingBlocks.API.csproj", "BuildingBlocks/API"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Application/HoopHub.BuildingBlocks.Application.csproj", "BuildingBlocks/Application"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Domain/HoopHub.BuildingBlocks.Domain.csproj", "BuildingBlocks/Domain"]
COPY ["BuildingBlocks/HoopHub.BuildingBlocks.Infrastructure/HoopHub.BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/Infrastructure"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Application/HoopHub.Modules.NBAData.Application.csproj", "Modules/NBAData/Application"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Domain/HoopHub.Modules.NBAData.Domain.csproj", "Modules/NBAData/Domain"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Infrastructure/HoopHub.Modules.NBAData.Infrastructure.csproj", "Modules/NBAData/Infrastructure"]

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