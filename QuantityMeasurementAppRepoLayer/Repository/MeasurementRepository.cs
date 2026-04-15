using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepoLayer.Interface;
using QuantityMeasurementAppRepoLayer.Context;

namespace QuantityMeasurementAppRepoLayer.Repository;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly AppDbContext _context;
    public MeasurementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveHistory(QuantityMeasurementHistoryEntity historyEntity)
    {
        _context.MeasurementHistories.Add(historyEntity);
        int rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }
    
}