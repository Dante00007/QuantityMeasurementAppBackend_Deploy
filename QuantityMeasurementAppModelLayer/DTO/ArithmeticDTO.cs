
namespace QuantityMeasurementAppModelLayer.DTO
{

    public class ArithmeticDTO
    {
        public QuantityDTO Quantity1 { get; set; }
        public QuantityDTO Quantity2 { get; set; }

        public ArithmeticDTO(QuantityDTO quantity1, QuantityDTO quantity2)
        {
            Quantity1 = quantity1;
            Quantity2 = quantity2;
        }
    }
}