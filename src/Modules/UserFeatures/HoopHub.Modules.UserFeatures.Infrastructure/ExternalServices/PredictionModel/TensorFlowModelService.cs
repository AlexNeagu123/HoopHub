using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.ExternalServices.AzureBlobStorage;
using HoopHub.Modules.UserFeatures.Application.ExternalServices.PredictionModel;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using Keras.Models;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Infrastructure.ExternalServices.PredictionModel
{
    public class TensorFlowModelService : ITensorFlowModelService
    {
        private readonly IAzureBlobStorageService _blobStorageService;
        private readonly string _blobName;
        private readonly string _localPath;
        private BaseModel? _model = null;
        private readonly ILogger<TensorFlowModelService> _logger;
        private readonly Lazy<Task> _initializeTask;

        public TensorFlowModelService(IAzureBlobStorageService blobStorageService, ILogger<TensorFlowModelService> logger)
        {
            _blobStorageService = blobStorageService;
            _logger = logger;
            _blobName = Config.KerasBlobName;
            _localPath = Path.Combine(Path.GetTempPath(), _blobName);
            _initializeTask = new Lazy<Task>(InitializeAsync);
        }

        private async Task InitializeAsync()
        {
            var downloadResult = await _blobStorageService.DownloadAsync(_blobName, _localPath);

            if (!downloadResult.IsSuccess)
                throw new Exception("Failed to download model: " + downloadResult.ErrorMsg);

            _model = BaseModel.LoadModel(_localPath);
            _logger.LogInformation("TensorFlow model loaded successfully.");
        }

        public async Task<BaseModel> GetModelAsync()
        {
            await _initializeTask.Value;
            if (_model == null)
                throw new Exception("Model initialization failed.");
            return _model;
        }

        public async Task<Result<float[]>> PredictAsync(float[] inputLayer)
        {
            var model = await GetModelAsync();
            var npInputData = Numpy.np.array(inputLayer).reshape(1, -1);
            var predictions = model.Predict(npInputData);
            return Result<float[]>.Success(predictions.GetData<float>());
        }
    }
}
