using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews.DeletePlayerPerformanceReview
{
    public class DeletePlayerPerformanceReviewCommandHandler(
        IPlayerPerformanceReviewRepository playerPerformanceReviewRepository,
        ICurrentUserService userService)
        : IRequestHandler<DeletePlayerPerformanceReviewCommand, BaseResponse>
    {
        private readonly IPlayerPerformanceReviewRepository _playerPerformanceReviewRepository = playerPerformanceReviewRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly PlayerPerformanceReviewMapper _playerPerformanceReviewMapper = new();

        public async Task<BaseResponse> Handle(DeletePlayerPerformanceReviewCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new DeletePlayerPerformanceReviewCommandValidator(_playerPerformanceReviewRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var playerPerformanceReview = await _playerPerformanceReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.PlayerId, request.Date, fanId!);
            if (!playerPerformanceReview.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(playerPerformanceReview.ErrorMsg, ValidationKeys.PlayerPerformanceReview);

            var deletePlayerPerformanceReviewResult = await _playerPerformanceReviewRepository.RemoveAsync(playerPerformanceReview.Value);
            return !deletePlayerPerformanceReviewResult.IsSuccess ? BaseResponse.ErrorResponseFromKeyMessage(deletePlayerPerformanceReviewResult.ErrorMsg, ValidationKeys.PlayerPerformanceReview) : new BaseResponse { Success = true };
        }
    }
}
