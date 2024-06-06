using FluentValidation;
using HoopHub.Modules.NBAData.Domain.Constants;
using HoopHub.Modules.NBAData.Domain.Rules;

namespace HoopHub.Modules.NBAData.Application.GamePredictions.GetGamePrediction
{
    public class GetGamePredictionQueryValidator : AbstractValidator<GetGamePredictionQuery>
    {
        public GetGamePredictionQueryValidator()
        {
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
        }
    }
}