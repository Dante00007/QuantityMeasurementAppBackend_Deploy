namespace QuantityMeasurementAppModelLayer.DTO
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string Unit { get; set; }

        public int CategoryIndex { get; set; }

        public QuantityDTO(double value, string unit,int categoryIndex)
        {
            Value = value;
            Unit = unit;
            CategoryIndex = categoryIndex;
        }
    }
}