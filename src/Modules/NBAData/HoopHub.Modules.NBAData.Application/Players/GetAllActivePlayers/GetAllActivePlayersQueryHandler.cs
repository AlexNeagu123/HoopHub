using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Application.Players.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetAllActivePlayers
{
    public class GetAllActivePlayersQueryHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAllActivePlayersQuery, Response<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly PlayerMapper _playerMapper = new();

        public async Task<Response<IReadOnlyList<PlayerDto>>> Handle(GetAllActivePlayersQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var allActivePlayersResult = await _playerRepository.GetAllActivePlayers();
            var playersDtoList = allActivePlayersResult.Value.Select(player => _playerMapper.PlayerToPlayerDto(player, isLicensed)).ToList();
            return new Response<IReadOnlyList<PlayerDto>>
            {
                Success = true,
                Data = playersDtoList
            };
        }
    }
}
