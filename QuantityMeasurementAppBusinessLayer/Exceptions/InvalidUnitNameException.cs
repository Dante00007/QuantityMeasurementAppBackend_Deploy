namespace QuantityMeasurementAppBusinessLayer.Exceptions;
public class InvalidUnitNameException : BaseBusinessLayerException
{
    public InvalidUnitNameException(string unit, string type) 
        : base($"'{unit}' is not a valid unit for {type}.", 400) { }
}