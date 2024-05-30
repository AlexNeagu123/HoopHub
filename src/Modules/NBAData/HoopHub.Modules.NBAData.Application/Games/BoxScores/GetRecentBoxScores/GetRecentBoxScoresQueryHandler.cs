using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetRecentBoxScores
{
    public class GetRecentBoxScoresQueryHandler(IBoxScoresRepository boxScoreRepository, ICurrentUserService userService)
        : IRequestHandler<GetRecentBoxScoresQuery, Response<IReadOnlyList<LocalStoredBoxScoresDto>>>
    {
        private readonly IBoxScoresRepository _boxScoreRepository = boxScoreRepository;
        private readonly LocalBoxScoresMapper _localBoxScoresMapper = new();
        private readonly ICurrentUserService _userService = userService;

        public async Task<Response<IReadOnlyList<LocalStoredBoxScoresDto>>> Handle(GetRecentBoxScoresQuery request, CancellationToken cancellationToken)
        {
            var isLicensed = _userService.GetUserLicense ?? false;
            var latestDate = await _boxScoreRepository.FindMostRecentGameDay();
            var boxScoresResult = await _boxScoreRepository.GetByDateAsync(latestDate.ToUniversalTime());
            if (!boxScoresResult.IsSuccess)
                return Response<IReadOnlyList<LocalStoredBoxScoresDto>>.ErrorResponseFromKeyMessage(boxScoresResult.ErrorMsg, ValidationKeys.BoxScores);

            var boxScores = boxScoresResult.Value;
            var boxScoresDto = boxScores
                .Select(bs => _localBoxScoresMapper.LocalStoredBoxScoresToLocalStoredBoxScoresDto(bs, isLicensed)).ToList();

            return new Response<IReadOnlyList<LocalStoredBoxScoresDto>>
            {
                Success = true,
                Data = boxScoresDto
            };
        }
    }
}
