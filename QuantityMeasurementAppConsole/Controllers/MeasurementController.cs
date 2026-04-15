using QuantityMeasurementAppBusinessLayer.Services;
using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppModelLayer.Units;

namespace QuantityMeasurementAppConsole.Controllers
{
    public class MeasurementController
    {
        private readonly IMeasurementService _service;

        public MeasurementController()
        {
            _service = new MeasurementService();
        }

        public void HandleConversion(QuantityDTO q, string toUnit) 
        {
            var result = _service.PerformConversion(q, toUnit);
            Console.WriteLine($"\n[RESULT] {q.Value} {q.Unit} is equal to {result.Value} {result.Unit}");
        }

        public void HandleAddition(QuantityDTO q1, QuantityDTO q2, string target) 
        {
            try
            {
                var result = _service.PerformAddition(q1,q2, target);
                Console.WriteLine($"\n[RESULT] {q1.Value} {q1.Unit} + {q2.Value} {q2.Unit} = {result.Value} {result.Unit}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n[ERROR] {ex.Message}");
                Console.WriteLine($"\n[ERROR] {ex.StackTrace}");
            }
        }

        public void HandleSubtraction(QuantityDTO q1, QuantityDTO q2, string target)
        {
            try
            {
                var result = _service.PerformSubtraction(q1,q2, target);
                Console.WriteLine($"\n[RESULT] {q1.Value} {q1.Unit} - {q2.Value} {q2.Unit} = {result.Value} {result.Unit}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n[ERROR] {ex.Message}");
            }
        }
    }
}