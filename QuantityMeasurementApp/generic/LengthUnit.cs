namespace QuantityMeasurementApp.generic
{
    public delegate bool SupportsArithmetic();

    public enum LengthUnit 
    {
        FEET,
        INCH,
        YARD,
        CENTIMETER
    }

    public static class LengthUnitExtensions 
    {
        private static readonly SupportsArithmetic _supportsArithmetic = () => true;

        public static void ValidateOperationSupport(this LengthUnit unit, string operation)
        {
            if (!_supportsArithmetic())
            {
                throw new InvalidOperationException($"{operation} is not supported for Length units.");
            }
        }

        public static double GetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 1.0,           
                LengthUnit.INCH => 1.0 / 12.0,    
                LengthUnit.YARD => 3.0,         
                LengthUnit.CENTIMETER => 1.0 / 30.48,
                _ => throw new ArgumentException("Invalid Unit")
            };
        }

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return value * unit.GetFactor();
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return baseValue / unit.GetFactor();
        }
    }
}