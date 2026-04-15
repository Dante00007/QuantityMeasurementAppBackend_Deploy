
using Azure.Core;
using QuantityMeasurementAppBusinessLayer.Exceptions;
using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepoLayer.Interface;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(string? userId)
        {
            if (userId == null) throw new AccessTokenException("No access token.", 401);
            return await _userRepository.GetHistory(Guid.Parse(userId));   
        }
    }
}