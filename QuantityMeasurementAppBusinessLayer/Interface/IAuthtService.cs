
using QuantityMeasurementAppModelLayer.DTO;

namespace QuantityMeasurementAppBusinessLayer.Interface
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDTO registerDTO);
        Task<LoginResponseDTO> Login(LoginDTO loginDTO);
        Task<LoginResponseDTO> Refresh(string userId,string refreshToken);
        Task Logout(string userId,string refreshToken);
    }
}