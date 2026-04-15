namespace QuantityMeasurementAppRepoLayer.Exceptions;
public class UserAlreadyExistsException : BaseRepoLayerException
{
    public UserAlreadyExistsException(string email) 
        : base($"The email '{email}' is already registered.", 409) { } 
}