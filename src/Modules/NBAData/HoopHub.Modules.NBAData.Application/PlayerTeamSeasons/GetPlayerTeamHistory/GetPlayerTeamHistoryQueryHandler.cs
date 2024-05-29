using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Dtos;
using HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQueryHandler(IPlayerTeamSeasonRepository playerTeamSeasonRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetPlayerTeamHistoryQuery, Response<IReadOnlyList<PlayerTeamSeasonDto>>>
    {
        private readonly IPlayerTeamSeasonRepository _playerTeamSeasonRepository = playerTeamSeasonRepository;
        private readonly PlayerTeamSeasonMapper _playerTeamSeasonMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<IReadOnlyList<PlayerTeamSeasonDto>>> Handle(GetPlayerTeamHistoryQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetPlayerTeamHistoryQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<PlayerTeamSeasonDto>>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _playerTeamSeasonRepository.GetTeamHistoryByPlayerId(request.PlayerId);
            var playerTeamHistory = queryResult.Value;
            var playerTeamHistoryDtoList = playerTeamHistory.Select(p => _playerTeamSeasonMapper.PlayerTeamSeasonToPlayerTeamSeasonDto(p, isLicensed)).ToList();

            return new Response<IReadOnlyList<PlayerTeamSeasonDto>>
            {
                Success = true,
                Data = playerTeamHistoryDtoList
            };
        }
    }
}
