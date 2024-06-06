using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.BuildingBlocks.Application.ExternalServices.PredictionsModel
{
    public interface ITensorFlowModelService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputLayer"></param>
        /// <returns>
        ///   Returns an array with 2 elements: The first one being the winning probability of the home team
        ///   and the second one being the winning probability of the visitor team
        /// </returns>
        Task<Result<float[]>> PredictAsync(float[] inputLayer);
    }
}