using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players
{
    public class GetAllPlayersQueryHandler(IPlayerRepository playerRepository)
        : IRequestHandler<GetAllPlayersQuery, Response<List<PlayerDto>>>
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;

        public Task<Response<List<PlayerDto>>> Handle(GetAllPlayersQuery request,
            CancellationToken cancellationToken)
        {
            var players = _playerRepository.GetAll().Value;
            var playerDtos = players.Select(p => new PlayerDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName
            }).ToList();

            return Task.FromResult(new Response<List<PlayerDto>>
            {
                Success = true,
                Data = playerDtos
            });
        }
    }
}
