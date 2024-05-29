using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam
{
    public class GetActivePlayersByTeamQueryHandler(IPlayerRepository playerRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetActivePlayersByTeamQuery, Response<IReadOnlyList<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly Mappers.PlayerMapper _playerMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;


        public async Task<Response<IReadOnlyList<PlayerDto>>> Handle(GetActivePlayersByTeamQuery request,
            CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetActivePlayersByTeamQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<PlayerDto>>.ErrorResponseFromFluentResult(validationResult);

            var queryResult = await _playerRepository.GetAllActivePlayersByTeam(request.TeamId);
            var players = queryResult.Value;
            var playerDtoList = players.Select(p => _playerMapper.PlayerToPlayerDto(p, isLicensed)).ToList();

            return new Response<IReadOnlyList<PlayerDto>>
            {
                Success = true,
                Data = playerDtoList
            };
        }
    }
}
