namespace QuantityMeasurementAppRepoLayer.Exceptions;

public class BaseRepoLayerException : Exception
{
    public int StatusCode { get; }
    public BaseRepoLayerException(string message, int statusCode=500) : base(message)
    {
        StatusCode = statusCode;
    }
}