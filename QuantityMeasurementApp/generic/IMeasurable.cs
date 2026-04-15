namespace QuantityMeasurementApp.generic;

// Step 2: Functional Interface


// Step 2: IMeasurable Interface
public interface IMeasurable
{
    double ConvertToBaseUnit(double value);
    double ConvertFromBaseUnit(double baseValue);
    
    // Default-like validation method
    void ValidateOperationSupport(string operation)
    {
        // Default implementation does nothing (supports all)
    }
}