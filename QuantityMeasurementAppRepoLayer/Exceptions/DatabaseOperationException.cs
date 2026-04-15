namespace QuantityMeasurementAppRepoLayer.Exceptions;
public class DatabaseOperationException : BaseRepoLayerException
{
    public DatabaseOperationException(string message) 
        : base(message, 500) { } 
}