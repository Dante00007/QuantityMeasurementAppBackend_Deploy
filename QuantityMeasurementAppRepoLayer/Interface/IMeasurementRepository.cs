
using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppRepoLayer.Interface;

public interface IMeasurementRepository
{
    public Task<bool> SaveHistory(QuantityMeasurementHistoryEntity entry);

}