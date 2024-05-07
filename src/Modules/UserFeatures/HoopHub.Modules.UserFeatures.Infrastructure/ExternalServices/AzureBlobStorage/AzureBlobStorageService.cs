using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.ExternalServices.AzureBlobStorage;
using HoopHub.Modules.UserFeatures.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HoopHub.Modules.UserFeatures.Infrastructure.ExternalServices.AzureBlobStorage
{
    public class AzureBlobStorageService(IConfiguration configuration) : IAzureBlobStorageService
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<Result<string>> UploadAsync(string fanId, IFormFile file)
        {
            if (!file.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase) || !Path.GetExtension(file.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                return Result<string>.Failure("Only PNG files are allowed.");
            }

            var connectionString = _configuration[Config.AzureConnectionStringKey];
            var containerName = _configuration[Config.AzureContainerNameKey];

            var blobContainerClient = new BlobContainerClient(connectionString, containerName);
            var blobClient = blobContainerClient.GetBlobClient(fanId);

            try
            {
                var blobExists = await blobClient.ExistsAsync();
                if (blobExists)
                {
                    var deleteResult = await blobClient.DeleteAsync();
                    if (deleteResult.IsError)
                    {
                        return Result<string>.Failure("Failed to delete the existing image.");
                    }
                }

                await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = file.ContentType
                    }
                });
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
                return Result<string>.Success(blobClient.Uri.AbsoluteUri);
            }
            catch (Exception e)
            {
                return Result<string>.Failure(e.Message);
            }
        }
    }
}
