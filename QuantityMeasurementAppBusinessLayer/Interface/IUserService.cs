using QuantityMeasurementAppModelLayer.Core;
using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public interface IUserService
    {
        Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(string? userId);
        // Task<QuantityMeasurementHistoryEntity> GetHistoryById(int id, string? token);
    }
}