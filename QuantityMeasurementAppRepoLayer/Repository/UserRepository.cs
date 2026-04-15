using Microsoft.EntityFrameworkCore;

using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppRepoLayer.Context;
using QuantityMeasurementAppRepoLayer.Interface;

namespace QuantityMeasurementAppRepoLayer.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveUser(UserEntity user)
    {
        _context.Users.Add(user);

        int rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected > 0;
    }

    public async Task<UserEntity?> VerifyUser(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(Guid someUserId)
    {
        var userWithHistory = _context.Users
        .Include(u => u.Histories)
        .FirstOrDefault(u => u.Id == someUserId);

        return userWithHistory?.Histories ?? new List<QuantityMeasurementHistoryEntity>();
    }

    public async Task<UserEntity?> GetUserById(Guid userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
}