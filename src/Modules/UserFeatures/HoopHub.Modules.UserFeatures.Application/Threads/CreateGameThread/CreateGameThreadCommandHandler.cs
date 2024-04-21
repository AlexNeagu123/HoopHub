using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateGameThread
{
    public class CreateGameThreadCommandHandler(IGameThreadRepository gameThreadRepository)
        : IRequestHandler<CreateGameThreadCommand, Response<GameThreadDto>>
    {
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly GameThreadMapper _gameThreadMapper = new();

        public async Task<Response<GameThreadDto>> Handle(CreateGameThreadCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateGameThreadCommandValidator(_gameThreadRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameThreadDto>.ErrorResponseFromFluentResult(validationResult);

            var gameThreadResult = GameThread.Create(request.HomeTeamId, request.VisitorTeamId, request.Date);
            if (!gameThreadResult.IsSuccess)
                return Response<GameThreadDto>.ErrorResponseFromKeyMessage(gameThreadResult.ErrorMsg, ValidationKeys.GameThread);

            var gameThread = gameThreadResult.Value;
            var createGameThreadResult = await _gameThreadRepository.AddAsync(gameThread);
            if (!createGameThreadResult.IsSuccess)
                return Response<GameThreadDto>.ErrorResponseFromKeyMessage(createGameThreadResult.ErrorMsg, ValidationKeys.GameThread);

            return new Response<GameThreadDto>
            {
                Data = _gameThreadMapper.GameThreadToGameThreadDto(gameThread),
                Success = true
            };
        }
    }
}
