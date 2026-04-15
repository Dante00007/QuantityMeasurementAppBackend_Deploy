namespace QuantityMeasurementApp;

using QuantityMeasurementApp.models;
public class QuantityMeasurementApplication
{
    public static Boolean DemonstrateLengthEquality(QuantityLength length1, QuantityLength length2)
    {
        return length1.Equals(length2);
    }

}