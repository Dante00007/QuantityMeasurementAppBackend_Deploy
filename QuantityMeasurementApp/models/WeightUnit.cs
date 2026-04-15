namespace QuantityMeasurementApp.models
{
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND
    }

    public static class WeightUnitExtensions
    {
        public static double GetFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.KILOGRAM => 1.0,           
                WeightUnit.GRAM => 0.001,    
                WeightUnit.POUND => 0.453592,         
                _ => throw new ArgumentException("Invalid Unit")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetFactor();
        }
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return baseValue / unit.GetFactor();
        }
    }
}