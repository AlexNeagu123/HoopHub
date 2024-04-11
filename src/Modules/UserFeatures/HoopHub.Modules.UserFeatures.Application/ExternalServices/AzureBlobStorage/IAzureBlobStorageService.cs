using HoopHub.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace HoopHub.Modules.UserFeatures.Application.ExternalServices.AzureBlobStorage
{
    public interface IAzureBlobStorageService
    {
        Task<Result<string>> UploadAsync(string fanId, IFormFile file);
    }
}
