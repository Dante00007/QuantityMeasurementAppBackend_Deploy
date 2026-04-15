using QuantityMeasurementAppModelLayer.Core;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppModelLayer.Units;

using QuantityMeasurementAppRepoLayer.Interface;


using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppBusinessLayer.Exceptions;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IJwtService _jwtService;
        private readonly IMeasurementRepository _repository;

        public MeasurementService(IMeasurementRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        private Enum ParseUnit(Type unitType, string unitName)
        {
            try
            {
                return (Enum)Enum.Parse(unitType, unitName, ignoreCase: true);
            }
            catch (ArgumentException)
            {
                throw new InvalidUnitNameException(unitName, unitType.Name);
            }
        }
        private Type GetUnitFromIndex(int index)
        {
            return index switch
            {
                1 => typeof(LengthUnit),
                2 => typeof(WeightUnit),
                3 => typeof(VolumeUnit),
                4 => typeof(TemperatureUnit),
                _ => throw new InvalidUnitException($"{index} is an Invalid index. In MeasurementService.cs"),
            };
        }
        public async Task<QuantityDTO> PerformConversion(QuantityDTO q, string toUnit, string? userId)
        {

            Type unitType = GetUnitFromIndex(q.CategoryIndex);
            Enum unit = ParseUnit(unitType, q.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity = new Quantity(q.Value, unit);
            QuantityDTO res = quantity.ConvertTo(targetUnit);

            Console.WriteLine(userId);
            if (userId != null)
                await AddToHistory(q, null, res, "Conversion", userId, unitType.Name);

            return res;
        }

        public async Task<QuantityDTO> PerformAddition(QuantityDTO q1, QuantityDTO q2, string toUnit, string? userId)
        {
            if (q1.CategoryIndex != q2.CategoryIndex)
                throw new IncompatibleUnitException("Cannot perform addition on different unit types.");

            Type unitType = GetUnitFromIndex(q1.CategoryIndex);
            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);
            QuantityDTO result = quantity1.Add(quantity2, targetUnit);

            if (userId != null)
                await AddToHistory(q1, q2, result, "Addition", userId, unitType.Name);
            return result;
        }

        public async Task<QuantityDTO> PerformSubtraction(QuantityDTO q1, QuantityDTO q2, string toUnit, string? userId)
        {
            if (q1.CategoryIndex != q2.CategoryIndex)
                throw new IncompatibleUnitException("Cannot perform addition on different unit types.");

            Type unitType = GetUnitFromIndex(q1.CategoryIndex);
            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);
            QuantityDTO result = quantity1.Subtract(quantity2, targetUnit);

            if (userId != null)
                await AddToHistory(q1, q2, result, "Addition", userId, unitType.Name);
            return result;
        }

        public async Task<QuantityDTO> PerformDivision(QuantityDTO q1, QuantityDTO q2, string toUnit, string? userId)
        {
            if (q1.CategoryIndex != q2.CategoryIndex)
                throw new IncompatibleUnitException("Cannot perform addition on different unit types.");

            Type unitType = GetUnitFromIndex(q1.CategoryIndex);
            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);
            Enum targetUnit = ParseUnit(unitType, toUnit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);
            QuantityDTO result = quantity1.Divide(quantity2, targetUnit);

            if (userId != null)
                await AddToHistory(q1, q2, result, "Division", userId, unitType.Name);
            return result;
        }
        public async Task<bool> PerformEquality(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.CategoryIndex != q2.CategoryIndex)
                throw new IncompatibleUnitException("Cannot perform addition on different unit types.");
            
            Type unitType = GetUnitFromIndex(q1.CategoryIndex);
            Enum unit1 = ParseUnit(unitType, q1.Unit);
            Enum unit2 = ParseUnit(unitType, q2.Unit);

            Quantity quantity1 = new Quantity(q1.Value, unit1);
            Quantity quantity2 = new Quantity(q2.Value, unit2);

            return quantity1.Equals(quantity2);
        }
        private async Task AddToHistory(QuantityDTO q1, QuantityDTO? q2, QuantityDTO result, string operation, string? userId, string category)
        {
            if (userId == null) return;
            QuantityMeasurementHistoryEntity history = new QuantityMeasurementHistoryEntity
            {
                InputValue1 = q1.Value,
                InputUnit1 = q1.Unit,
                InputValue2 = q2?.Value ?? 0,
                InputUnit2 = q2?.Unit ?? "",
                TargetUnit = result.Unit,
                Operation = operation,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                Category = category,
                UserId = Guid.Parse(userId)
            };

            await _repository.SaveHistory(history);
        }


    }
}