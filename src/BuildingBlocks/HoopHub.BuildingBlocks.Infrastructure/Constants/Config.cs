namespace HoopHub.BuildingBlocks.Infrastructure.Constants
{
    public class Config
    {
        public const string AzureConnectionStringKey = "AzureBlobStorage:ConnectionString";
        public const string AzurePhotosContainerNameKey = "AzureBlobStorage:PhotosContainerName";
        public const string MlContainerNameKey = "AzureBlobStorage:MLContainerName";
        public const string KerasBlobName = "hoophub_model.onnx";
        public const string NormalizationParams = "normalization_params.json";
    }
}
