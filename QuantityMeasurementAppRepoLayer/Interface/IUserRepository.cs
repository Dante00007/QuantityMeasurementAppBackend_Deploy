
using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppRepoLayer.Interface;

public interface IUserRepository 
{
    public Task<bool> SaveUser(UserEntity user); 
    public Task<UserEntity?> VerifyUser(string email); 
    public Task<UserEntity?> GetUserById(Guid userId);
    public Task<ICollection<QuantityMeasurementHistoryEntity>> GetHistory(Guid token);
}