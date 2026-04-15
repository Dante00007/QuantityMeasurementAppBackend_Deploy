namespace QuantityMeasurementAppBusinessLayer.Exceptions;
public class IncompatibleUnitException : BaseBusinessLayerException
{
    public IncompatibleUnitException(string message) 
        : base(message, 400) { }
}