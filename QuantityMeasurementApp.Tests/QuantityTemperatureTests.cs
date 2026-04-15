namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.generic;
using System;

[TestClass]
public class TemperatureRefactoringTests
{
    private const double Epsilon = 0.001;

    [TestMethod]
    public void TestTemperatureEquality_CelsiusToCelsius_SameValue()
    {
        var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void TestTemperatureEquality_FahrenheitToFahrenheit_SameValue()
    {
        var t1 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
        var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
        Assert.IsTrue(t1.Equals(t2));
    }

    [TestMethod]
    public void TestTemperatureEquality_CelsiusToFahrenheit_0Celsius32Fahrenheit()
    {
        var celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
        var fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
        Assert.IsTrue(celsius.Equals(fahrenheit));
    }

    [TestMethod]
    public void TestTemperatureEquality_CelsiusToFahrenheit_100Celsius212Fahrenheit()
    {
        var celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.FAHRENHEIT);
        Assert.IsTrue(celsius.Equals(fahrenheit));
    }

    [TestMethod]
    public void TestTemperatureEquality_CelsiusToFahrenheit_Negative40Equal()
    {
        var celsius = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.CELSIUS);
        var fahrenheit = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.FAHRENHEIT);
        Assert.IsTrue(celsius.Equals(fahrenheit));
    }

    [TestMethod]
    public void TestTemperatureEquality_SymmetricProperty()
    {
        var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
        Assert.IsTrue(t1.Equals(t2) && t2.Equals(t1));
    }

    [TestMethod]
    public void TestTemperatureEquality_ReflexiveProperty()
    {
        var t1 = new Quantity<TemperatureUnit>(25.0, TemperatureUnit.CELSIUS);
        Assert.IsTrue(t1.Equals(t1));
    }

    [TestMethod]
    public void TestTemperatureConversion_CelsiusToFahrenheit_VariousValues()
    {
        var t1 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
        Assert.AreEqual(122.0, t1.ConvertTo(TemperatureUnit.FAHRENHEIT).Value, Epsilon);
    }

    [TestMethod]
    public void TestTemperatureConversion_FahrenheitToCelsius_VariousValues()
    {
        var t1 = new Quantity<TemperatureUnit>(122.0, TemperatureUnit.FAHRENHEIT);
        Assert.AreEqual(50.0, t1.ConvertTo(TemperatureUnit.CELSIUS).Value, Epsilon);
    }

    [TestMethod]
    public void TestTemperatureConversion_RoundTrip_PreservesValue()
    {
        var original = new Quantity<TemperatureUnit>(37.0, TemperatureUnit.CELSIUS);
        var roundTrip = original.ConvertTo(TemperatureUnit.FAHRENHEIT).ConvertTo(TemperatureUnit.CELSIUS);
        Assert.AreEqual(original.Value, roundTrip.Value, Epsilon);
    }

    [TestMethod]
    public void TestTemperatureUnsupportedOperation_Add()
    {
        var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
        Assert.Throws<InvalidOperationException>(() => t1.Add(t2));
    }

    [TestMethod]
    public void TestTemperatureUnsupportedOperation_Subtract()
    {
        var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
        Assert.Throws<InvalidOperationException>(() => t1.Subtract(t2));
    }

    [TestMethod]
    public void TestTemperatureUnsupportedOperation_Divide()
    {
        var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
        Assert.Throws<InvalidOperationException>(() => t1.Divide(t2));
    }

    [TestMethod]
    public void TestTemperatureUnsupportedOperation_ErrorMessage()
    {
        var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var ex = Assert.Throws<InvalidOperationException>(() => t1.Add(t1));
        Assert.AreEqual("Addition is not supported for Temperature units.", ex.Message);
    }

    [TestMethod]
    public void TestTemperatureVsLengthIncompatibility()
    {
        var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        var l1 = new Quantity<LengthUnit>(100.0, LengthUnit.FEET);
        Assert.IsFalse(t1.Equals(l1));
    }

    [TestMethod]
    public void TestOperationSupportMethods_TemperatureUnitAddition()
    {
        Assert.Throws<InvalidOperationException>(() => TemperatureUnit.CELSIUS.ValidateOperationSupport("Addition"));
    }

    [TestMethod]
    public void TestOperationSupportMethods_LengthUnitAddition()
    {
        LengthUnit.FEET.ValidateOperationSupport("Addition");
    }

    [TestMethod]
    public void TestTemperatureNullUnitValidation()
    {
        Assert.Throws<ArgumentException>(() => new Quantity<TemperatureUnit>(100.0, (TemperatureUnit)(object)null!));
    }

    [TestMethod]
    public void TestTemperatureDifferentValuesInequality()
    {
        var t1 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
        var t2 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
        Assert.IsFalse(t1.Equals(t2));
    }

    [TestMethod]
    public void TestTemperatureValidateOperationSupport_MethodBehavior()
    {
        Assert.Throws<InvalidOperationException>(() => TemperatureUnit.CELSIUS.ValidateOperationSupport("addition"));
    }

    [TestMethod]
    public void TestTemperatureIntegrationWithGenericQuantity()
    {
        var temp = new Quantity<TemperatureUnit>(37.0, TemperatureUnit.CELSIUS);
        Assert.AreEqual(37.0, temp.Value);
        Assert.AreEqual(TemperatureUnit.CELSIUS, temp.Unit);
    }
}