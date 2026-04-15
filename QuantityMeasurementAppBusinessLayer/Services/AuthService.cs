using Microsoft.AspNetCore.Identity;

using QuantityMeasurementAppRepoLayer.Interface;

using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppModelLayer.Entity;

using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppBusinessLayer.Exceptions;
using QuantityMeasurementAppRepoLayer.Exceptions;


namespace QuantityMeasurementAppBusinessLayer.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly PasswordHasher<UserEntity> _passwordHasher;

    public AuthService(IJwtService jwtService, IUserRepository repository, ITokenRepository tokenRepository)
    {
        _jwtService = jwtService;
        _userRepository = repository;
        _tokenRepository = tokenRepository;
        _passwordHasher = new PasswordHasher<UserEntity>();
    }

    public async Task<bool> Register(RegisterDTO registerDTO)
    {
        var fullName = registerDTO.FullName;
        var password = registerDTO.Password;
        var email = registerDTO.Email;
        var phone = registerDTO.Phone;


        UserEntity user = new UserEntity();

        user.FullName = fullName;
        user.Email = email;
        user.Phone = phone;
        user.Password = _passwordHasher.HashPassword(user, password);

        return await _userRepository.SaveUser(user);
    }

    public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
    {
        var email = loginDTO.Email;
        var password = loginDTO.Password;

        UserEntity? user = await _userRepository.VerifyUser(email);

        if (user != null &&
         _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
        {
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();
            await _tokenRepository.SaveRefreshTokenAsync(user.Id, refreshToken);
            return new LoginResponseDTO { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        throw new PasswordNotMatchException("Password Not Match");
    }

    public async Task<LoginResponseDTO> Refresh(string? token, string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken)) throw new RefreshTokenException("No refresh token.", 400);
        if (string.IsNullOrEmpty(token))
        {
            throw new AccessTokenException("User already logged out.",400);
        }
        Guid userId = _jwtService.ExtractUserIdFromToken(token);
        Console.WriteLine(userId);
        UserEntity? user = await _userRepository.GetUserById(userId);
        if (user == null) throw new UserNotFoundException("User not found.");

        var isValid = await _tokenRepository.ValidateRefreshTokenAsync(user.Id, refreshToken);

        if (!isValid)
        {
            // IF REFRESH TOKEN IS EXPIRED/INVALID
            throw new RefreshTokenException("Session expired. Please log in again.", 401);
        }

        var accessToken = _jwtService.GenerateAccessToken(user);

        return new LoginResponseDTO { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task Logout(string? userId, string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(userId))
        {
           return;
        }
        // if (string.IsNullOrEmpty(userId))
        // {
        //     throw new UserNotFoundException("User already logged out.");
        // }

        await _tokenRepository.DeleteRefreshTokenAsync(Guid.Parse(userId), refreshToken);
    }
}