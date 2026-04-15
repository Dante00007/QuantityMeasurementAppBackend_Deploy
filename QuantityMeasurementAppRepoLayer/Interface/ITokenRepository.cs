
namespace QuantityMeasurementAppRepoLayer.Interface
{
    public interface ITokenRepository
    {
        Task SaveRefreshTokenAsync(Guid userId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
        Task DeleteRefreshTokenAsync(Guid userId, string refreshToken);
        

    }
}