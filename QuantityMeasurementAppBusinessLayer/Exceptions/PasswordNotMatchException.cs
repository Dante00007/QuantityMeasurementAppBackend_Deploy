
namespace QuantityMeasurementAppBusinessLayer.Exceptions;

public class PasswordNotMatchException : BaseBusinessLayerException
{
    public PasswordNotMatchException(string message) : base(message, 401) { }
}