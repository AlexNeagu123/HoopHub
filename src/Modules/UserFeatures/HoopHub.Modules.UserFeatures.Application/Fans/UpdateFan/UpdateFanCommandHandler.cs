using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.ExternalServices.AzureBlobStorage;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Fans.UpdateFan
{
    public class UpdateFanCommandHandler(IAzureBlobStorageService storageService, IFanRepository fanRepository, ICurrentUserService currentUserService) : IRequestHandler<UpdateFanCommand, Response<FanDto>>
    {
        private readonly IAzureBlobStorageService _storageService = storageService;
        private readonly IFanRepository _fanRepository = fanRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly FanMapper _fanMapper = new();
        public async Task<Response<FanDto>> Handle(UpdateFanCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFanCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<FanDto>.ErrorResponseFromFluentResult(validationResult);

            var currentUserId = _currentUserService.GetUserId;
            if (string.IsNullOrEmpty(currentUserId))
                return Response<FanDto>.ErrorResponseFromKeyMessage(ValidationErrors.InvalidFanId, ValidationKeys.FanId);

            var fanResult = await _fanRepository.FindByIdAsync(currentUserId);
            if (!fanResult.IsSuccess)
                return Response<FanDto>.ErrorResponseFromKeyMessage(fanResult.ErrorMsg, ValidationKeys.FanId);

            var fan = fanResult.Value;

            if (request.ProfileImage is not null)
            {
                var profileImageUrlResult = await _storageService.UploadAsync(currentUserId, request.ProfileImage);
                if (!profileImageUrlResult.IsSuccess)
                    return Response<FanDto>.ErrorResponseFromKeyMessage(profileImageUrlResult.ErrorMsg, ValidationKeys.ProfileImage);
                
                fan.UpdateAvatarPhotoUrl(profileImageUrlResult.Value);
                var updateFanResult = await _fanRepository.UpdateAsync(fan);
                if (!updateFanResult.IsSuccess)
                    return Response<FanDto>.ErrorResponseFromKeyMessage(updateFanResult.ErrorMsg, ValidationKeys.FanUpdate);
            }

            return new Response<FanDto>
            {
                Success = true,
                Data = _fanMapper.FanToFanDto(fan)
            };
        }
    }
}
