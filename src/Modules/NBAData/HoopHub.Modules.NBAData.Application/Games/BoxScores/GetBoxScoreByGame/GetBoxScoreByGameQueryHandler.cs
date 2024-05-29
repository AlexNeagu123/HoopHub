using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoreByGame
{
    public class GetBoxScoreByGameQueryHandler(IBoxScoresDataService boxScoresDataService, ITeamRepository teamRepository, IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetBoxScoreByGameQuery, Response<GameWithBoxScoreDto>>
    {
        private readonly IBoxScoresDataService _boxScoresDataService = boxScoresDataService;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<GameWithBoxScoreDto>> Handle(GetBoxScoreByGameQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetBoxScoreByGameQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromFluentResult(validationResult);

            var apiBoxScoresResult = await _boxScoresDataService.GetBoxScoresAsyncByDate(request.Date);
            if (apiBoxScoresResult.IsSuccess == false)
                return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(apiBoxScoresResult.ErrorMsg, ValidationKeys.BoxScores);

            var apiBoxScores = apiBoxScoresResult.Value;
            foreach (var boxScore in apiBoxScores)
            {
                if (boxScore.HomeTeam == null || boxScore.VisitorTeam == null) continue;
                if (boxScore.HomeTeam.Id != request.HomeTeamApiId || boxScore.VisitorTeam.Id != request.VisitorTeamApiId)
                    continue;

                BoxScoreProcessor boxScoreProcessor = new(_teamRepository, _playerRepository, isLicensed);
                return await boxScoreProcessor.ProcessApiBoxScoreAndConvert(boxScore);
            }

            return Response<GameWithBoxScoreDto>.ErrorResponseFromKeyMessage(ErrorMessages.BoxScoreNotFound, ValidationKeys.BoxScores);
        }
    }
}
