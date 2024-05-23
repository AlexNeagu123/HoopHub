using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByPlayer
{
    public class GetBoxScoresByPlayerQueryHandler(IBoxScoresRepository boxScoresRepository)
        : IRequestHandler<GetBoxScoresByPlayerQuery, Response<IReadOnlyList<LocalStoredBoxScoresDto>>>
    {
        private readonly IBoxScoresRepository _boxScoresRepository = boxScoresRepository;
        private readonly LocalBoxScoresMapper _boxScoresMapper = new();

        public async Task<Response<IReadOnlyList<LocalStoredBoxScoresDto>>> Handle(GetBoxScoresByPlayerQuery request,
            CancellationToken cancellationToken)
        {
            var validator = new GetBoxScoresByPlayerQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<LocalStoredBoxScoresDto>>.ErrorResponseFromFluentResult(validationResult);

            var boxScores = await _boxScoresRepository.GetLastXBoxScoresByPlayerId(request.PlayerId, 5);
            if (!boxScores.IsSuccess)
                return Response<IReadOnlyList<LocalStoredBoxScoresDto>>.ErrorResponseFromKeyMessage(boxScores.ErrorMsg, ValidationKeys.BoxScores);

            var boxScoresDtoList = boxScores.Value.Select(bs => _boxScoresMapper.LocalStoredBoxScoresToLocalStoredBoxScoresDto(bs)).ToList();
            return new Response<IReadOnlyList<LocalStoredBoxScoresDto>>
            {
                Success = true,
                Data = boxScoresDtoList
            };
        }
    }
}
