using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetGameThreadsByDate
{
    public class GetGameThreadsByDateQueryHandler(IGameThreadRepository gameThreadRepository)
        : IRequestHandler<GetGameThreadsByDateQuery, Response<IReadOnlyList<GameThreadDto>>>
    {
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly GameThreadMapper _gameThreadMapper = new();

        public async Task<Response<IReadOnlyList<GameThreadDto>>> Handle(GetGameThreadsByDateQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetGameThreadsByDateQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<GameThreadDto>>.ErrorResponseFromFluentResult(validationResult);

            var gameThreadsResult = await _gameThreadRepository.GetAllByDateAsync(request.Date);
            if (!gameThreadsResult.IsSuccess)
                return Response<IReadOnlyList<GameThreadDto>>.ErrorResponseFromKeyMessage(gameThreadsResult.ErrorMsg, ValidationKeys.GameThread);

            var gameThreads = gameThreadsResult.Value;
            var gameThreadDtos = gameThreads.Select(_gameThreadMapper.GameThreadToGameThreadDto).ToList();

            return new Response<IReadOnlyList<GameThreadDto>>
            {
                Data = gameThreadDtos,
                Success = true
            };
        }
    }
}
