using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam
{
    public class GetActivePlayersByTeamQueryHandler(IPlayerRepository playerRepository)
        : IRequestHandler<GetActivePlayersByTeamQuery, Response<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly PlayerMapper _playerMapper = new();

        public async Task<Response<IReadOnlyList<PlayerDto>>> Handle(GetActivePlayersByTeamQuery request,
            CancellationToken cancellationToken)
        {
            var validator = new GetActivePlayersByTeamQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<PlayerDto>>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _playerRepository.GetAllActivePlayersByTeam(request.TeamId);
            var players = queryResult.Value;
            var playerDtoList = players.Select(p => _playerMapper.PlayerToPlayerDto(p)).ToList();

            return new Response<IReadOnlyList<PlayerDto>>
            {
                Success = true,
                Data = playerDtoList
            };
        }
    }
}
