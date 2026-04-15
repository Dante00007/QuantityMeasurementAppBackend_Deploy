using QuantityMeasurementApp.generic;

namespace QuantityMeasurementApp.generic
{
    public sealed class Quantity<U> where U : Enum
    {
        private readonly double _value;
        private readonly U _unit;

        public Quantity(double value, U unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid measurement value");

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public U Unit => _unit;

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        private void ValidateArithmeticOperands(Quantity<U> other, U targetUnit, bool targetRequired, string operation)
        {
            if (other == null)
                throw new ArgumentException("Operand cannot be null");

            if (targetRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            // Step 2 & 4: Check for Temperature arithmetic support
            if (_unit is TemperatureUnit tempUnit)
                tempUnit.ValidateOperationSupport(operation);

            if (double.IsNaN(_value) || double.IsInfinity(_value))
                throw new ArgumentException("Invalid value");

            if (double.IsNaN(other._value) || double.IsInfinity(other._value))
                throw new ArgumentException("Invalid value");
        }

        private double ConvertToBase()
        {
            if (_unit is LengthUnit lengthUnit)
                return lengthUnit.ConvertToBaseUnit(_value);

            if (_unit is WeightUnit weightUnit)
                return weightUnit.ConvertToBaseUnit(_value);

            if (_unit is VolumeUnit volumeUnit)
                return volumeUnit.ConvertToBaseUnit(_value);

            if (_unit is TemperatureUnit tempUnit)
                return tempUnit.ConvertToBaseUnit(_value);

            throw new ArgumentException("Unsupported unit type");
        }

        private Quantity<U> ConvertFromBase(double baseValue, U targetUnit)
        {
            double result;

            if (targetUnit is LengthUnit lengthUnit)
                result = lengthUnit.ConvertFromBaseUnit(baseValue);

            else if (targetUnit is WeightUnit weightUnit)
                result = weightUnit.ConvertFromBaseUnit(baseValue);

            else if (targetUnit is VolumeUnit volumeUnit)
                result = volumeUnit.ConvertFromBaseUnit(baseValue);

            else if (targetUnit is TemperatureUnit tempUnit)
                result = tempUnit.ConvertFromBaseUnit(baseValue);

            else
                throw new ArgumentException("Unsupported unit");

            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            double baseValue = ConvertToBase();
            return ConvertFromBase(baseValue, targetUnit);
        }

        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            double base1 = ConvertToBase();
            double base2 = other.ConvertToBase();

            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return base1 + base2;

                case ArithmeticOperation.SUBTRACT:
                    return base1 - base2;

                case ArithmeticOperation.DIVIDE:
                    if (base2 == 0)
                        throw new ArithmeticException("Division by zero");
                    return base1 / base2;

                default:
                    throw new ArgumentException("Invalid operation");
            }
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Addition");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            return ConvertFromBase(result, _unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true, "Addition");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            return ConvertFromBase(result, targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Subtraction");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            return ConvertFromBase(result, _unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true, "Subtraction");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            return ConvertFromBase(result, targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Division");
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Quantity<U>))
                return false;

            Quantity<U> other = (Quantity<U>)obj;
            return Math.Abs(ConvertToBase() - other.ConvertToBase()) < 0.0001;
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}