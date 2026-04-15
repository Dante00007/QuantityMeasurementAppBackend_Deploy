using Microsoft.Extensions.Caching.Distributed;
using QuantityMeasurementAppRepoLayer.Interface;
using System.Text.Json;

namespace QuantityMeasurementAppRepoLayer.Repository;
public class TokenRepository : ITokenRepository
{
    private readonly IDistributedCache _cache;

    public TokenRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SaveRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        
        string key = $"RT_{userId}_{refreshToken}";
       
        await _cache.SetStringAsync(key, userId.ToString(), options);
    }

    public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        string key = $"RT_{userId}_{refreshToken}";
        var storedUserId = await _cache.GetStringAsync(key);
        
        return storedUserId != null && storedUserId == userId.ToString();
    }

    public async Task DeleteRefreshTokenAsync(Guid userId, string refreshToken)
    {
        string key = $"RT_{userId}_{refreshToken}";
        await _cache.RemoveAsync(key);
    }
}