using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Reviews;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.CreateGameReview
{
    public class CreateGameReviewCommandHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService) : 
        IRequestHandler<CreateGameReviewCommand, Response<GameReviewDto>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly GameReviewMapper _gameReviewMapper = new();
        public async Task<Response<GameReviewDto>> Handle(CreateGameReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _currentUserService.GetUserId;
            var validator = new CreateGameReviewCommandValidator(_gameReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var gameReviewResult = GameReview.Create(request.HomeTeamId, request.VisitorTeamId, request.Date, fanId!, request.Rating, request.Content);
            if (!gameReviewResult.IsSuccess)
                return Response<GameReviewDto>.ErrorResponseFromKeyMessage(gameReviewResult.ErrorMsg, ValidationKeys.GameReview);

            var gameReview = gameReviewResult.Value;
            gameReview.MarkAsAdded();

            var addResult = await _gameReviewRepository.AddAsync(gameReview);
            if (!addResult.IsSuccess)
                return Response<GameReviewDto>.ErrorResponseFromKeyMessage(addResult.ErrorMsg, ValidationKeys.GameReview);

            var addedGameReview = await _gameReviewRepository.FindByIdAsyncIncludingAll(gameReview.HomeTeamId, gameReview.VisitorTeamId, gameReview.Date, gameReview.FanId);
            return new Response<GameReviewDto>
            {
                Success = true,
                Data = _gameReviewMapper.GameReviewToGameReviewDto(addedGameReview.Value, null)
            };
        }
    }
}
