using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.GamePredictions.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction
{
    public class GetGamePredictionQuery : IRequest<Response<GamePredictionDto>>
    {
        public string Date { get; set; } = null!;
        public int HomeTeamId { get; set; }
        public int VisitorTeamId { get; set; }
    }
}