namespace QuantityMeasurementApp.generic
{
    public enum TemperatureUnit 
    {
        CELSIUS,
        FAHRENHEIT
    }

    public static class TemperatureUnitExtensions 
    {
        private static readonly SupportsArithmetic _supportsArithmetic = () => false;
        private static readonly Func<double, double> CELSIUS_TO_CELSIUS = (celsius) => celsius;

        public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
        {
            if (!_supportsArithmetic())
            {
                throw new InvalidOperationException($"{operation} is not supported for Temperature units.");
            }
        }

        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => CELSIUS_TO_CELSIUS(value),
                TemperatureUnit.FAHRENHEIT => (value - 32.0) * 5.0 / 9.0,
                _ => throw new ArgumentException("Invalid Unit")
            };
        }

        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => baseValue,
                TemperatureUnit.FAHRENHEIT => (baseValue * 9.0 / 5.0) + 32.0,
                _ => throw new ArgumentException("Invalid Unit")
            };
        }
    }
}