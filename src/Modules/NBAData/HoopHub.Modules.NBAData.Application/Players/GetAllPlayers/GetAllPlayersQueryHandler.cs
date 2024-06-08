using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Application.Players.Mappers;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetAllPlayers
{
    public class GetAllPlayersQueryHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetAllPlayersQuery, Response<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly PlayerMapper _playerMapper = new();

        public Task<Response<IReadOnlyList<PlayerDto>>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var allActivePlayersResult = _playerRepository.GetAll();
            var playersDtoList = allActivePlayersResult.Value.Select(player => _playerMapper.PlayerToPlayerDto(player, isLicensed)).ToList();

            return Task.FromResult(new Response<IReadOnlyList<PlayerDto>>
            {
                Success = true,
                Data = playersDtoList
            });
        }
    }
}
