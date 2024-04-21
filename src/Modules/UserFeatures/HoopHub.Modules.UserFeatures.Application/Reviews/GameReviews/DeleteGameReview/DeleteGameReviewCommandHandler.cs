using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.DeleteGameReview
{
    public class DeleteGameReviewCommandHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService currentUserService) : IRequestHandler<DeleteGameReviewCommand, BaseResponse>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly GameReviewMapper _gameReviewMapper = new();
        public async Task<BaseResponse> Handle(DeleteGameReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _currentUserService.GetUserId;
            var validator = new DeleteGameReviewCommandValidator(_gameReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var gameReviewResult = await _gameReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.Date, fanId!);
            if (!gameReviewResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(gameReviewResult.ErrorMsg, ValidationKeys.GameReview);

            var gameReview = gameReviewResult.Value;
            var deleteResult = await _gameReviewRepository.RemoveAsync(gameReview);
            return !deleteResult.IsSuccess ? BaseResponse.ErrorResponseFromKeyMessage(deleteResult.ErrorMsg, ValidationKeys.GameReview) : new BaseResponse { Success = true };
        }
    }
}
