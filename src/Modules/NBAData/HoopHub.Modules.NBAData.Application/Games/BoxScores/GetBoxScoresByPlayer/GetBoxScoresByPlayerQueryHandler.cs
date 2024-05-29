using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByPlayer
{
    public class GetBoxScoresByPlayerQueryHandler(IBoxScoresRepository boxScoresRepository, ICurrentUserService currentUserService)
        : IRequestHandler<GetBoxScoresByPlayerQuery, Response<IReadOnlyList<LocalStoredBoxScoresDto>>>
    {
        private readonly IBoxScoresRepository _boxScoresRepository = boxScoresRepository;
        private readonly LocalBoxScoresMapper _boxScoresMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Response<IReadOnlyList<LocalStoredBoxScoresDto>>> Handle(GetBoxScoresByPlayerQuery request,
            CancellationToken cancellationToken)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var validator = new GetBoxScoresByPlayerQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredBoxScoresDto>>.ErrorResponseFromFluentResult(validationResult);

            var boxScores = await _boxScoresRepository.GetLastXBoxScoresByPlayerId(request.PlayerId, request.GameCount);
            if (!boxScores.IsSuccess)
                return Response<IReadOnlyList<LocalStoredBoxScoresDto>>.ErrorResponseFromKeyMessage(boxScores.ErrorMsg, ValidationKeys.BoxScores);

            var boxScoresDtoList = boxScores.Value.Select(bs => _boxScoresMapper.LocalStoredBoxScoresToLocalStoredBoxScoresDto(bs, isLicensed)).ToList();
            return new Response<IReadOnlyList<LocalStoredBoxScoresDto>>
            {
                Success = true,
                Data = boxScoresDtoList
            };
        }
    }
}
