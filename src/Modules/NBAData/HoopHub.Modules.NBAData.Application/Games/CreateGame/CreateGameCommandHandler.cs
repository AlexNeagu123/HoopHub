using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Domain.Games;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.CreateGame
{
    public class CreateGameCommandHandler(ITeamRepository teamRepository, ISeasonRepository seasonRepository, IGameRepository gameRepository, ICurrentUserService currentUserService)
        : IRequestHandler<CreateGameCommand, Response<LocalStoredGameDto>>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly ISeasonRepository _seasonRepository = seasonRepository;
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly LocalGameMapper _gameMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<LocalStoredGameDto>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var homeTeamResult = await _teamRepository.FindByApiIdAsync(request.HomeTeamApiId);
            if (!homeTeamResult.IsSuccess)
                return Response<LocalStoredGameDto>.ErrorResponseFromKeyMessage(homeTeamResult.ErrorMsg, ValidationKeys.Games);

            var visitorTeamResult = await _teamRepository.FindByApiIdAsync(request.VisitorTeamApiId);
            if (!visitorTeamResult.IsSuccess)
                return Response<LocalStoredGameDto>.ErrorResponseFromKeyMessage(visitorTeamResult.ErrorMsg, ValidationKeys.Games);

            var seasonResult = await _seasonRepository.FindBySeasonPeriod(request.SeasonPeriod);
            if (!seasonResult.IsSuccess)
                return Response<LocalStoredGameDto>.ErrorResponseFromKeyMessage(seasonResult.ErrorMsg, ValidationKeys.Games);

            var game = Game.Create(
                request.Date,
                homeTeamResult.Value.Id,
                visitorTeamResult.Value.Id,
                request.HomeTeamScore,
                request.VisitorTeamScore,
                request.Status,
                seasonResult.Value.Id,
                request.Postseason,
                request.Time,
                request.Period);

            game.MarkGameAdded(homeTeamResult.Value.FullName, visitorTeamResult.Value.FullName, homeTeamResult.Value.ImageUrl,
                visitorTeamResult.Value.ImageUrl, homeTeamResult.Value.ApiId, visitorTeamResult.Value.ApiId);

            var addGameResult = await _gameRepository.AddAsync(game);
            if (!addGameResult.IsSuccess)
                return Response<LocalStoredGameDto>.ErrorResponseFromKeyMessage(addGameResult.ErrorMsg, ValidationKeys.Games);

            var addedGameResult = await _gameRepository.FindByIdAsync(addGameResult.Value.Id);
            if (!addedGameResult.IsSuccess)
                return Response<LocalStoredGameDto>.ErrorResponseFromKeyMessage(addedGameResult.ErrorMsg, ValidationKeys.Games);

            return new Response<LocalStoredGameDto>
            {
                Success = true,
                Data = _gameMapper.LocalStoredGameToLocalStoredGameDto(addedGameResult.Value, isLicensed)
            };
        }
    }
}
