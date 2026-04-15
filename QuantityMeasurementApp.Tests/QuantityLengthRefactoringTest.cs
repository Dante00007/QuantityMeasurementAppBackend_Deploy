namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.models;
using System;

[TestClass]
public class QuantityLengthRefactoringTest
{
    private const double Epsilon = 1e-4;


    [TestMethod]
    public void TestLengthUnitEnum_FeetConstant()
    {
        Assert.AreEqual(1.0, LengthUnit.FEET.GetFactor());
    }

    [TestMethod]
    public void TestLengthUnitEnum_InchesConstant()
    {
        Assert.AreEqual(0.0833, LengthUnit.INCH.GetFactor(), Epsilon);
    }

    [TestMethod]
    public void TestLengthUnitEnum_YardsConstant()
    {
        Assert.AreEqual(3.0, LengthUnit.YARD.GetFactor());
    }

    [TestMethod]
    public void TestLengthUnitEnum_CentimetersConstant()
    {
        Assert.AreEqual(0.0328, LengthUnit.CENTIMETER.GetFactor(), Epsilon);
    }

 

    [TestMethod]
    public void TestConvertToBaseUnit_FeetToFeet()
    {
        Assert.AreEqual(5.0, LengthUnit.FEET.ConvertToBaseUnit(5.0));
    }

    [TestMethod]
    public void TestConvertToBaseUnit_InchesToFeet()
    {
        Assert.AreEqual(1.0, LengthUnit.INCH.ConvertToBaseUnit(12.0), Epsilon);
    }

    [TestMethod]
    public void TestConvertToBaseUnit_YardsToFeet()
    {
        Assert.AreEqual(3.0, LengthUnit.YARD.ConvertToBaseUnit(1.0));
    }

    [TestMethod]
    public void TestConvertToBaseUnit_CentimetersToFeet()
    {
        Assert.AreEqual(1.0, LengthUnit.CENTIMETER.ConvertToBaseUnit(30.48), Epsilon);
    }

   
    [TestMethod]
    public void TestConvertFromBaseUnit_FeetToFeet()
    {
        Assert.AreEqual(2.0, LengthUnit.FEET.ConvertFromBaseUnit(2.0));
    }

    [TestMethod]
    public void TestConvertFromBaseUnit_FeetToInches()
    {
        Assert.AreEqual(12.0, LengthUnit.INCH.ConvertFromBaseUnit(1.0), Epsilon);
    }

    [TestMethod]
    public void TestConvertFromBaseUnit_FeetToYards()
    {
        Assert.AreEqual(1.0, LengthUnit.YARD.ConvertFromBaseUnit(3.0));
    }

    [TestMethod]
    public void TestConvertFromBaseUnit_FeetToCentimeters()
    {
        Assert.AreEqual(30.48, LengthUnit.CENTIMETER.ConvertFromBaseUnit(1.0), Epsilon);
    }



    [TestMethod]
    public void TestQuantityLengthRefactored_Equality()
    {
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        var inch = new QuantityLength(12.0, LengthUnit.INCH);
        Assert.IsTrue(feet.Equals(inch));
    }

    [TestMethod]
    public void TestQuantityLengthRefactored_Add()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.FEET);
        
        Assert.AreEqual(2.0, result.Value, Epsilon);
        Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void TestQuantityLengthRefactored_AddWithTargetUnit()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.YARD);

        Assert.AreEqual(0.6667, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestQuantityLengthRefactored_InvalidValue()
    {
        Assert.Throws<ArgumentException>(() => 
            QuantityLength.Convert(double.NaN, LengthUnit.FEET, LengthUnit.INCH));
    }
   

    [TestMethod]
    public void TestRoundTripConversion_RefactoredDesign()
    {
        double original = 10.0;
        double feetToCm = QuantityLength.Convert(original, LengthUnit.FEET, LengthUnit.CENTIMETER);
        double cmToFeet = QuantityLength.Convert(feetToCm, LengthUnit.CENTIMETER, LengthUnit.FEET);
        
        Assert.AreEqual(original, cmToFeet, Epsilon);
    }

    [TestMethod]
    public void TestUnitImmutability()
    {
        
        double factor1 = LengthUnit.YARD.GetFactor();
        double factor2 = LengthUnit.YARD.GetFactor();
        Assert.AreEqual(factor1, factor2);
    }

    
}