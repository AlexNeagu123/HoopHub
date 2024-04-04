using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.BoxScoreas;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetGamesByTeam
{
    public class GetBoxScoresByTeamQueryHandler(IBoxScoresDataService boxScoresDataService, ITeamRepository teamRepository, IPlayerRepository playerRepository) : IRequestHandler<GetBoxScoresByTeamQuery, Response<IReadOnlyList<GameWithBoxScoreDto>>>
    {
        private readonly IBoxScoresDataService _boxScoresDataService = boxScoresDataService;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;

        public async Task<Response<IReadOnlyList<GameWithBoxScoreDto>>> Handle(GetBoxScoresByTeamQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBoxScoresByTeamQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameWithBoxScoreDto>>.ErrorResponseFromFluentResult(validationResult);

            List<GameWithBoxScoreDto> lastGames = [];
            var currentDate = DateTime.Now;
            while (lastGames.Count < request.GameCount)
            {
                var dateStr = currentDate.ToString("yyyy-MM-dd");
                var boxScoreByTeam = await _boxScoresDataService.GetBoxScoresAsyncByTeamAndDate(dateStr, request.ApiId);
                currentDate = currentDate.AddDays(-1);
                if(!boxScoreByTeam.IsSuccess)
                    continue;

                BoxScoreProcessor boxScoreProcessor = new(_boxScoresDataService, _teamRepository, _playerRepository);
                var processResponse = await boxScoreProcessor.ProcessApiBoxScoreAndConvert(boxScoreByTeam.Value);
                if(processResponse.Success)
                {
                    lastGames.Add(processResponse.Data);
                }
            }

            return new Response<IReadOnlyList<GameWithBoxScoreDto>> 
            {
                Success = true, 
                Data = lastGames        
            };
        }
    }
}
