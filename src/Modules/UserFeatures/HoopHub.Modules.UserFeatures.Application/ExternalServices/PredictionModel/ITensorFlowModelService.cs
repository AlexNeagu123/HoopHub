using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Application.ExternalServices.PredictionModel
{
    public interface ITensorFlowModelService
    {
        Task<Result<float[]>> PredictAsync(float[] inputLayer);
    }
}
