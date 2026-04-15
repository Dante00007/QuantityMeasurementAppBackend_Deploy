namespace QuantityMeasurementAppBusinessLayer.Exceptions;

public class BaseBusinessLayerException : Exception
{
    public int StatusCode { get; }

    public BaseBusinessLayerException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}