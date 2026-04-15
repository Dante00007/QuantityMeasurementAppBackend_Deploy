namespace QuantityMeasurementAppBusinessLayer.Exceptions;

public class InvalidUnitException : BaseBusinessLayerException
{
    public InvalidUnitException(string message) : base(message,400) { }
}