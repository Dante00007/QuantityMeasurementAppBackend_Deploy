using QuantityMeasurementAppModelLayer.Units;
using QuantityMeasurementAppModelLayer.DTO;

namespace QuantityMeasurementAppModelLayer.Core
{
    public sealed class Quantity
    {
        private readonly double _value;
        private readonly Enum _unit;

        public Quantity(double value, Enum unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid measurement value");

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public Enum Unit => _unit;

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        private void ValidateArithmeticOperands(Quantity other, Enum targetUnit, bool targetRequired, string operation)
        {
            if (other == null)
                throw new ArgumentException("Operand cannot be null");

            if (targetRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

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

        private Quantity ConvertFromBase(double baseValue,Enum targetUnit)
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

            return new Quantity(Math.Round(result, 2), targetUnit);
        }

        public QuantityDTO ConvertTo(Enum targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            double baseValue = ConvertToBase();
            Quantity result = ConvertFromBase(baseValue, targetUnit);

            return new QuantityDTO(result.Value, result.Unit.ToString(),-1);
        }

        private double PerformBaseArithmetic(Quantity other, ArithmeticOperation operation)
        {
            double base1 = ConvertToBase();
            double base2 = other.ConvertToBase();

            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return base1 + base2;

                case ArithmeticOperation.SUBTRACT:
                    return Math.Abs(base1 - base2);

                case ArithmeticOperation.DIVIDE:
                    if (base2 == 0)
                        throw new ArithmeticException("Division by zero");
                    return base1 / base2;

                default:
                    throw new ArgumentException("Invalid operation");
            }
        }

        public QuantityDTO Add(Quantity other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Addition");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            Quantity quantity = ConvertFromBase(result, _unit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }

        public QuantityDTO Add(Quantity other, Enum targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true, "Addition");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            
            Quantity quantity = ConvertFromBase(result, targetUnit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }

        public QuantityDTO Subtract(Quantity other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Subtraction");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            Quantity quantity = ConvertFromBase(result, _unit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }

        public QuantityDTO Subtract(Quantity other, Enum targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true, "Subtraction");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            Quantity quantity = ConvertFromBase(result, targetUnit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }

        public QuantityDTO Divide(Quantity other)
        {
            ValidateArithmeticOperands(other, _unit, false, "Division");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
            Quantity quantity = ConvertFromBase(result, _unit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }
        public QuantityDTO Divide(Quantity other, Enum targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true, "Subtraction");
            double result = PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
            Quantity quantity = ConvertFromBase(result, targetUnit);
            return new QuantityDTO(quantity.Value, quantity.Unit.ToString(),-1);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Quantity))
                return false;

            Quantity other = (Quantity)obj;
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