using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.GetGamesByTeam;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam
{
    public class GetBoxScoresByTeamQueryHandler(IBoxScoresDataService boxScoresDataService, ITeamRepository teamRepository, IPlayerRepository playerRepository, ITeamLatestRepository teamLatestRepository) : IRequestHandler<GetBoxScoresByTeamQuery, Response<IReadOnlyList<GameWithBoxScoreDto>>>
    {
        private readonly IBoxScoresDataService _boxScoresDataService = boxScoresDataService;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly ITeamLatestRepository _teamLatestRepository = teamLatestRepository;

        public async Task<Response<IReadOnlyList<GameWithBoxScoreDto>>> Handle(GetBoxScoresByTeamQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetBoxScoresByTeamQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameWithBoxScoreDto>>.ErrorResponseFromFluentResult(validationResult);

            List<GameWithBoxScoreDto> lastGames = [];
            var latestGamesResult = await _teamLatestRepository.GetLatestByTeam(request.TeamId);
            if (!latestGamesResult.IsSuccess)
                return Response<IReadOnlyList<GameWithBoxScoreDto>>.ErrorResponseFromKeyMessage(latestGamesResult.ErrorMsg, ValidationKeys.TeamLatest);
            
            var latestGames = latestGamesResult.Value;
            foreach (var game in latestGames)
            {
                var boxScoreByTeam = await _boxScoresDataService.GetBoxScoresAsyncByTeamAndDate(game.GameDate, game.Team.ApiId);
                if(!boxScoreByTeam.IsSuccess)
                    continue;

                BoxScoreProcessor boxScoreProcessor = new(_teamRepository, _playerRepository);
                var processResponse = await boxScoreProcessor.ProcessApiBoxScoreAndConvert(boxScoreByTeam.Value);
                if (processResponse.Success)
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
