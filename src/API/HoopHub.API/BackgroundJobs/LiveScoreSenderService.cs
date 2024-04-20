using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.BoxScores;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Persistence;

namespace HoopHub.API.BackgroundJobs
{
    public class LiveScoreGetterService
    {
        public static async Task<Response<IReadOnlyList<GameWithBoxScoreDto>>> GetLiveBoxScores(ITeamRepository teamRepository, IPlayerRepository playerRepository, IBoxScoresDataService boxScoresDataService)
        {
            var boxScores = await boxScoresDataService.GetLiveBoxScores();
            if (!boxScores.IsSuccess)
            {
                return new Response<IReadOnlyList<GameWithBoxScoreDto>>
                {
                    Success = false,
                    Data = null!
                };
            }

            var boxScoreProcessor = new BoxScoreProcessor(teamRepository, playerRepository);
            var liveBoxScores = boxScores.Value;

            List<GameWithBoxScoreDto> liveProcessedBoxScores = [];

            foreach (var liveBoxScore in liveBoxScores)
            {
                var boxScore = await boxScoreProcessor.ProcessApiBoxScoreAndConvert(liveBoxScore);
                liveProcessedBoxScores.Add(boxScore.Data);
            }


            return new Response<IReadOnlyList<GameWithBoxScoreDto>>
            {
                Success = true,
                Data = liveProcessedBoxScores
            };
        }
    }
}
