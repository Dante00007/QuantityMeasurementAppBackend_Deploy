namespace QuantityMeasurementAppModelLayer.Units
{
    public enum VolumeUnit
    {
        LITRE,
        MILLILITRE,
        GALLON
    }

    public static class VolumeUnitExtensions
    {
        private static readonly SupportsArithmetic _supportsArithmetic = () => true;

        public static void ValidateOperationSupport(this VolumeUnit unit, string operation)
        {
            if (!_supportsArithmetic())
            {
                throw new InvalidOperationException($"{operation} is not supported for Volume units.");
            }
        }

        public static double GetFactor(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.LITRE => 1.0,
                VolumeUnit.MILLILITRE => 0.001,
                VolumeUnit.GALLON => 3.78541,
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return value * unit.GetFactor();
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return baseValue / unit.GetFactor();
        }
    }
}