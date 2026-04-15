namespace QuantityMeasurementApp.Tests;


using QuantityMeasurementApp.models;


[TestClass]
public class QuantityLengthConversionTests
{
    private const double Epsilon = 1e-6;

    [TestMethod]
    public void TestConversion_FeetToInches()
    {
        double result = QuantityLength.Convert(1.0, LengthUnit.FEET, LengthUnit.INCH);
        Assert.AreEqual(12.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_InchesToFeet()
    {
        double result = QuantityLength.Convert(24.0, LengthUnit.INCH, LengthUnit.FEET);
        Assert.AreEqual(2.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_YardsToInches()
    {
        double result = QuantityLength.Convert(1.0, LengthUnit.YARD, LengthUnit.INCH);
        Assert.AreEqual(36.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_InchesToYards()
    {
        double result = QuantityLength.Convert(72.0, LengthUnit.INCH, LengthUnit.YARD);
        Assert.AreEqual(2.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_CentimetersToInches()
    {
        double result = QuantityLength.Convert(2.54, LengthUnit.CENTIMETER, LengthUnit.INCH);
        Assert.AreEqual(1.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_FeetToYard()
    {
        double result = QuantityLength.Convert(6.0, LengthUnit.FEET, LengthUnit.YARD);
        Assert.AreEqual(2.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_RoundTrip_PreservesValue()
    {
        double originalValue = 5.0;
        double intermediate = QuantityLength.Convert(originalValue, LengthUnit.FEET, LengthUnit.CENTIMETER);
        double finalValue = QuantityLength.Convert(intermediate, LengthUnit.CENTIMETER, LengthUnit.FEET);

        Assert.AreEqual(originalValue, finalValue, Epsilon);
    }

    [TestMethod]
    public void TestConversion_ZeroValue()
    {
        double result = QuantityLength.Convert(0.0, LengthUnit.FEET, LengthUnit.INCH);
        Assert.AreEqual(0.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_NegativeValue()
    {
        double result = QuantityLength.Convert(-1.0, LengthUnit.FEET, LengthUnit.INCH);
        Assert.AreEqual(-12.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_NaN_Throws()
    {
        Assert.Throws<ArgumentException>(() => 
            QuantityLength.Convert(double.NaN, LengthUnit.FEET, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestConversion_PositiveInfinity_Throws()
    {
        Assert.Throws<ArgumentException>(() => 
            QuantityLength.Convert(double.PositiveInfinity, LengthUnit.FEET, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestConversion_NegativeInfinity_Throws()
    {
        Assert.Throws<ArgumentException>(() => 
            QuantityLength.Convert(double.NegativeInfinity, LengthUnit.FEET, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestConversion_PrecisionTolerance()
    {
        double result = QuantityLength.Convert(1.0, LengthUnit.CENTIMETER, LengthUnit.INCH);
        double expected = 0.393700787; 
        Assert.AreEqual(expected, result, 1e-4); 
    }
}