using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQueryHandler(IPlayerTeamSeasonRepository playerTeamSeasonRepository)
        : IRequestHandler<GetPlayerTeamHistoryQuery, Response<IReadOnlyList<PlayerTeamSeasonDto>>>
    {
        private readonly IPlayerTeamSeasonRepository _playerTeamSeasonRepository = playerTeamSeasonRepository;
        private readonly PlayerTeamSeasonMapper _playerTeamSeasonMapper = new();

        public async Task<Response<IReadOnlyList<PlayerTeamSeasonDto>>> Handle(GetPlayerTeamHistoryQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetPlayerTeamHistoryQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<PlayerTeamSeasonDto>>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _playerTeamSeasonRepository.GetTeamHistoryByPlayerId(request.PlayerId);
            var playerTeamHistory = queryResult.Value;
            var playerTeamHistoryDtoList = playerTeamHistory.Select(p => _playerTeamSeasonMapper.PlayerTeamSeasonToPlayerTeamSeasonDto(p)).ToList();

            return new Response<IReadOnlyList<PlayerTeamSeasonDto>>
            {
                Success = true,
                Data = playerTeamHistoryDtoList
            };
        }
    }
}
