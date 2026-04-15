

namespace QuantityMeasurementAppRepoLayer.Exceptions;

public class UserNotFoundException : BaseRepoLayerException
{
    public UserNotFoundException(string message) : base(message,404) { }
}