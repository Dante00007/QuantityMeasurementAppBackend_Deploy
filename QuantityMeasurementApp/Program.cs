using QuantityMeasurementApp.generic;

try
{
    Console.WriteLine("--- Weight Operations ---");
    Quantity<WeightUnit> weight1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM); 
    Quantity<WeightUnit> weight2 = new Quantity<WeightUnit>(500, WeightUnit.GRAM);
    var weightSub = weight1.Subtract(weight2);
    Console.WriteLine($"Subtraction (1kg - 500g): {weightSub.Value} {weightSub.Unit}");
    double weightDiv = weight1.Divide(weight2);
    Console.WriteLine($"Division (1kg / 500g): {weightDiv}");

    Console.WriteLine("\n--- Length Operations ---");
    Quantity<LengthUnit> length1 = new Quantity<LengthUnit>(1, LengthUnit.FEET); 
    Quantity<LengthUnit> length2 = new Quantity<LengthUnit>(6, LengthUnit.INCH);
    var lengthSub = length1.Subtract(length2);
    Console.WriteLine($"Subtraction (1ft - 6in): {lengthSub.Value} {lengthSub.Unit}");
    double lengthDiv = length1.Divide(length2);
    Console.WriteLine($"Division (1ft / 6in): {lengthDiv}");

    Console.WriteLine("\n--- Volume Operations ---");
    Quantity<VolumeUnit> vol1 = new Quantity<VolumeUnit>(1, VolumeUnit.GALLON); 
    Quantity<VolumeUnit> vol2 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
    var volSub = vol1.Subtract(vol2, VolumeUnit.LITRE);
    Console.WriteLine($"Subtraction (1gal - 1L) in Litres: {volSub.Value} {volSub.Unit}");
    var convertedVol = vol1.ConvertTo(VolumeUnit.MILLILITRE);
    Console.WriteLine($"Conversion (1gal to mL): {convertedVol.Value} {convertedVol.Unit}");

    Console.WriteLine("\n--- Temperature Operations (Conversion) ---");
    Quantity<TemperatureUnit> cel = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
    var toFah = cel.ConvertTo(TemperatureUnit.FAHRENHEIT);
    Console.WriteLine($"Conversion (100°C to °F): {toFah.Value} {toFah.Unit}"); // Should be 212

    Quantity<TemperatureUnit> fah = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
    var toCel = fah.ConvertTo(TemperatureUnit.CELSIUS);
    Console.WriteLine($"Conversion (32°F to °C): {toCel.Value} {toCel.Unit}"); // Should be 0

    Console.WriteLine("\n--- Temperature Operations (Arithmetic Support Check) ---");
    try 
    {
        Console.WriteLine("Attempting to add two temperatures (100°C + 0°C)...");
        cel.Add(toCel); 
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Caught Expected Error: {ex.Message}");
    }

    Console.WriteLine("\n--- Equality Checks ---");
    Quantity<LengthUnit> feet = new Quantity<LengthUnit>(1, LengthUnit.FEET);
    Quantity<LengthUnit> inch = new Quantity<LengthUnit>(12, LengthUnit.INCH);
    Console.WriteLine($"1 Feet Equals 12 Inches: {feet.Equals(inch)}");

    Quantity<TemperatureUnit> t1 = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
    Quantity<TemperatureUnit> t2 = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
    Console.WriteLine($"0°C Equals 32°F: {t1.Equals(t2)}");
}
catch (ArithmeticException ex)
{
    Console.WriteLine($"Math Error: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Validation Error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected Error: {ex.GetType().Name} - {ex.Message}");
}