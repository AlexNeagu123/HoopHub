using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.CreateGame
{
    public class CreateGameCommand : IRequest<Response<LocalStoredGameDto>>
    {
        public DateTime Date { get; set; }
        public int HomeTeamApiId { get; set; }
        public int VisitorTeamApiId { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorTeamScore { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SeasonPeriod { get; set; } = string.Empty;
        public bool Postseason { get; set; }
        public string Time { get; set; } = string.Empty;
        public int Period { get; set; }
    }
}