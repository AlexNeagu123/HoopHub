using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class ImageUrlCannotBeEmpty(string imageUrl) : IBusinessRule
    {
        private readonly string _imageUrl = imageUrl;

        public bool IsBroken() => string.IsNullOrWhiteSpace(_imageUrl);

        public string Message => ValidationErrors.InvalidImageUrl;
    }
}
