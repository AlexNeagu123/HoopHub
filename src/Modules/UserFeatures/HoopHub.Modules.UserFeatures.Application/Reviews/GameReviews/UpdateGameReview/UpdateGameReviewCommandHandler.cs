using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.UpdateGameReview
{
    public class UpdateGameReviewCommandHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateGameReviewCommand, Response<GameReviewDto>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly GameReviewMapper _gameReviewMapper = new();

        public async Task<Response<GameReviewDto>> Handle(UpdateGameReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _currentUserService.GetUserId;
            var validator = new UpdateGameReviewCommandValidator(_gameReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var gameReviewResult = await _gameReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.Date, fanId!);
            if (!gameReviewResult.IsSuccess)
                return Response<GameReviewDto>.ErrorResponseFromKeyMessage(gameReviewResult.ErrorMsg, ValidationKeys.GameReview);

            var gameReview = gameReviewResult.Value;
            gameReview.Update(request.Rating);

            var updateGameReviewResult = await _gameReviewRepository.UpdateAsync(gameReview);
            if (!updateGameReviewResult.IsSuccess)
                return Response<GameReviewDto>.ErrorResponseFromKeyMessage(updateGameReviewResult.ErrorMsg, ValidationKeys.GameReview);

            return new Response<GameReviewDto>
            {
                Success = true,
                Data = _gameReviewMapper.GameReviewToGameReviewDto(updateGameReviewResult.Value, null)
            };
        }
    }
}
