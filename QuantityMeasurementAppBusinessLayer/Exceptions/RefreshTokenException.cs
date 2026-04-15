namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class RefreshTokenException : BaseBusinessLayerException
    {
        public RefreshTokenException(string message, int statusCode = 401) : base(message, statusCode) { }
    }
}