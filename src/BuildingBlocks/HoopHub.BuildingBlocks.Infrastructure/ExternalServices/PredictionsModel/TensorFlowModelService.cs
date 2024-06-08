using HoopHub.BuildingBlocks.Application.ExternalServices.AzureStorage;
using HoopHub.BuildingBlocks.Application.ExternalServices.PredictionsModel;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.ML.OnnxRuntime;
using Newtonsoft.Json;

namespace HoopHub.BuildingBlocks.Infrastructure.ExternalServices.PredictionsModel
{
    public class TensorFlowModelService : ITensorFlowModelService
    {
        private readonly IAzureBlobStorageService _blobStorageService;
        private readonly string _modelBlobName;
        private readonly string _normalizationParamsName;
        private readonly ILogger<TensorFlowModelService> _logger;
        private readonly Lazy<Task> _initializeTask;

        private InferenceSession? _inferenceSession;
        private NormalizationParams? _normalizationParams;

        public TensorFlowModelService(IAzureBlobStorageService blobStorageService, ILogger<TensorFlowModelService> logger)
        {
            _blobStorageService = blobStorageService;
            _logger = logger;
            _modelBlobName = Config.KerasBlobName;
            _normalizationParamsName = Config.NormalizationParams;
            _initializeTask = new Lazy<Task>(InitializeAsync);
        }

        private async Task InitializeAsync()
        {
            var downloadResult = await _blobStorageService.DownloadAsync(_modelBlobName, Path.Combine(Path.GetTempPath(), _modelBlobName));
            if (!downloadResult.IsSuccess)
                throw new Exception("Failed to download model: " + downloadResult.ErrorMsg);

            _inferenceSession = new InferenceSession(Path.Combine(Path.GetTempPath(), _modelBlobName));
            _logger.LogInformation("TensorFlow model loaded successfully.");

            downloadResult = await _blobStorageService.DownloadAsync(_normalizationParamsName, Path.Combine(Path.GetTempPath(), _normalizationParamsName));
            if (!downloadResult.IsSuccess)
                throw new Exception("Failed to download model: " + downloadResult.ErrorMsg);

            var normalizationParamsJson = await File.ReadAllTextAsync(Path.Combine(Path.GetTempPath(), _normalizationParamsName));
            _normalizationParams = JsonConvert.DeserializeObject<NormalizationParams>(normalizationParamsJson);

            if (_normalizationParams != null)
            {
                _logger.LogInformation("Normalization parameters loaded successfully.");
            }
            else
                throw new Exception("Failed to parse normalization parameters.");
        }

        public async Task<InferenceSession> GetSessionAsync()
        {
            await _initializeTask.Value;
            if (_inferenceSession == null)
                throw new Exception("Inference session initialization failed.");
            return _inferenceSession;
        }

        public async Task<NormalizationParams> GetNormalizationParams()
        {
            await _initializeTask.Value;
            if (_normalizationParams == null)
                throw new Exception("Normalization params initialization failed.");
            return _normalizationParams;
        }

        public async Task<Result<float[]>> PredictAsync(float[] inputLayer)
        {
            var session = await GetSessionAsync();
            var normalizationParams = await GetNormalizationParams();

            inputLayer = NormalizeInput(inputLayer, normalizationParams);
            return Result<float[]>.Success(PredictAsync(session, inputLayer).Value);
        }

        private static Result<float[]> PredictAsync(InferenceSession session, float[] inputLayer)
        {
            var dimensions = new long[] { 1, 588 };
            using var inputOrtValue = OrtValue.CreateTensorValueFromMemory(inputLayer, dimensions);

            var inputs = new Dictionary<string, OrtValue>
            {
                { "Input", inputOrtValue }
            };

            using var runOptions = new RunOptions();
            using var results = session.Run(runOptions, inputs, session.OutputNames);

            var output = results[0];
            var outputData = output.GetTensorDataAsSpan<float>();

            var homeTeamWinProbability = (float)Math.Truncate(outputData[0] * 100) / 100;
            var visitorTeamWinProbability = 1.0f - homeTeamWinProbability;

            return Result<float[]>.Success([homeTeamWinProbability, visitorTeamWinProbability]);
        }

        private static float[] NormalizeInput(IReadOnlyList<float> inputLayer, NormalizationParams normalizationParams)
        {
            if (inputLayer.Count != normalizationParams.Means.Length || inputLayer.Count != normalizationParams.Stds.Length)
                throw new ArgumentException("Input data length must match the length of normalization parameters.");

            var normalizedInput = new float[inputLayer.Count];
            for (var i = 0; i < inputLayer.Count; i++)
            {
                normalizedInput[i] = (inputLayer[i] - normalizationParams.Means[i]) / normalizationParams.Stds[i];
            }

            return normalizedInput;
        }
    }
}
