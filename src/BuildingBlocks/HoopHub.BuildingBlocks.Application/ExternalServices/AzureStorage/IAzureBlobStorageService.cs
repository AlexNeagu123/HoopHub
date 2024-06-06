using HoopHub.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace HoopHub.BuildingBlocks.Application.ExternalServices.AzureStorage
{
    public interface IAzureBlobStorageService
    {
        Task<Result<string>> UploadAsync(string fanId, IFormFile file);
        Task<Result<string>> DownloadAsync(string blobName, string localPath);
    }
}