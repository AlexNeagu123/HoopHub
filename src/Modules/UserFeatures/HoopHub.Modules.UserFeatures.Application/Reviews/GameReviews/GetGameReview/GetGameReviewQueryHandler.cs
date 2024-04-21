using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Dtos;
using HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews.GetGameReview
{
    public class GetGameReviewQueryHandler(IGameReviewRepository gameReviewRepository, ICurrentUserService userService)
        : IRequestHandler<GetGameReviewQuery, Response<GameReviewDto>>
    {
        private readonly IGameReviewRepository _gameReviewRepository = gameReviewRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly GameReviewMapper _gameReviewMapper = new();

        public async Task<Response<GameReviewDto>> Handle(GetGameReviewQuery request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new GetGameReviewQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<GameReviewDto>.ErrorResponseFromFluentResult(validationResult);

            var gameReviewResult = await _gameReviewRepository.FindByIdAsyncIncludingAll(request.HomeTeamId, request.VisitorTeamId, request.Date, fanId!);
            if (!gameReviewResult.IsSuccess)
            {
                return new Response<GameReviewDto>
                {
                    Success = true,
                    Data = new GameReviewDto { HomeTeamId = request.HomeTeamId, VisitorTeamId = request.VisitorTeamId, Date = request.Date }
                };
            };

            return new Response<GameReviewDto>
            {
                Success = true,
                Data = _gameReviewMapper.GameReviewToGameReviewDto(gameReviewResult.Value)
            };
        }
    }
}
