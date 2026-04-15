namespace QuantityMeasurementAppBusinessLayer.Exceptions
{
    public class AccessTokenException : BaseBusinessLayerException
    {
        public AccessTokenException(string message,int statuscode) : base(message,statuscode) { }
    }
}