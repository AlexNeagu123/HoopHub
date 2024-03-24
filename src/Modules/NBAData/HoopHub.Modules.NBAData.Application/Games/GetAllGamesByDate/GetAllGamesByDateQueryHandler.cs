using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Teams;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate
{
    public class GetAllGamesByDateQueryHandler(IGamesDataService gamesDataService, ITeamRepository teamRepository) 
        : IRequestHandler<GetAllGamesByDateQuery, Response<IReadOnlyList<GameDto>>>
    {
        private readonly IGamesDataService _gamesDataService = gamesDataService;
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly TeamMapper _teamMapper = new();
        private readonly GameMapper _gameMapper = new();

        public async Task<Response<IReadOnlyList<GameDto>>> Handle(GetAllGamesByDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllGamesByDateQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameDto>>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _gamesDataService.GetGamesByDateAsync(request.Date);
            if(!queryResult.IsSuccess)
                return Response<IReadOnlyList<GameDto>>.ErrorResponseFromKeyMessage(queryResult.ErrorMsg, ValidationKeys.Games);
            
            var games = queryResult.Value;
            var gameList = new List<GameDto>();

            foreach (var game in games)
            {
                var homeTeam = await _teamRepository.FindByApiIdAsync(game.HomeTeam.Id);
                if (!homeTeam.IsSuccess)
                    return Response<IReadOnlyList<GameDto>>.ErrorResponseFromKeyMessage(homeTeam.ErrorMsg, ValidationKeys.TeamApiId);
              
                var visitorTeam = await _teamRepository.FindByApiIdAsync(game.VisitorTeam.Id);
                if (!visitorTeam.IsSuccess)
                    return Response<IReadOnlyList<GameDto>>.ErrorResponseFromKeyMessage(visitorTeam.ErrorMsg, ValidationKeys.TeamApiId);

                var gameDto = _gameMapper.GameApiDtoToGameDto(game, _teamMapper.TeamToTeamDto(homeTeam.Value), _teamMapper.TeamToTeamDto(visitorTeam.Value));
                gameList.Add(gameDto);
            }

            return new Response<IReadOnlyList<GameDto>>
            {
                Success = true,
                Data = gameList
            };
        }
    }
}
