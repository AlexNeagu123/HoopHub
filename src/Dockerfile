FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base 
WORKDIR /app
EXPOSE 80 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src 

COPY ["API/HoopHub.API/HoopHub.API.csproj", "API/HoobHub.API/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Application/HoopHub.Modules.NBAData.Application.csproj", "Modules/NBAData/Application/"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Domain/HoopHub.Modules.NBAData.Domain.csproj", "Modules/NBAData/Domain"]
COPY ["Modules/NBAData/HoopHub.Modules.NBAData.Infrastructure/HoopHub.Modules.NBAData.Infrastructure.csproj", "Modules/NBAData/Infrastructure"]

RUN dotnet restore "API/HoobHub.API/HoopHub.API.csproj"
COPY . .

WORKDIR "/src/API/HoopHub.API"
RUN dotnet build "HoopHub.API.csproj" -c Release -o /app/build 

FROM build AS publish 
RUN dotnet publish "HoopHub.API.csproj" -c Release -o /app/publish 

FROM base as final 
WORKDIR /app 
COPY --from=publish /app/publish . 

ADD entrypoint.sh / 
ENTRYPOINT ["/bin/bash", "/entrypoint.sh"] 
