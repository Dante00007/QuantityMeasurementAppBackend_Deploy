using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface IJwtService
    {
        public string GenerateAccessToken(UserEntity user);
        public string GenerateRefreshToken();
        public Guid ExtractUserIdFromToken(string token);
    }
}