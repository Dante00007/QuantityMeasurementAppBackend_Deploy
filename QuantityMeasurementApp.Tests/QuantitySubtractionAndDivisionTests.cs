namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.generic;

[TestClass]
public class QuantitySubtractionAndDivisionTests
{
    private const double Epsilon = 1e-4;

    [TestMethod]
    public void TestSubtraction_SameUnit_FeetMinusFeet()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        var result = Quantity<LengthUnit>.Subtract(q1, q2);
        Assert.AreEqual(5.0, result.Value, Epsilon);
        Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void TestSubtraction_SameUnit_LitreMinusLitre()
    {
        var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(3.0, VolumeUnit.LITRE);
        var result = Quantity<VolumeUnit>.Subtract(q1, q2);
        Assert.AreEqual(7.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_CrossUnit_FeetMinusInches()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(6.0, LengthUnit.INCH);
        var result = Quantity<LengthUnit>.Subtract(q1, q2);
        Assert.AreEqual(9.5, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_ExplicitTargetUnit_Millilitre()
    {
        var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
        var result = Quantity<VolumeUnit>.Subtract(q1, q2, VolumeUnit.MILLILITRE);
        Assert.AreEqual(3000.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_ResultingInNegative()
    {
        var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var result = Quantity<LengthUnit>.Subtract(q1, q2);
        Assert.AreEqual(-5.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_WithNegativeValues()
    {
        var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(-2.0, LengthUnit.FEET);
        var result = Quantity<LengthUnit>.Subtract(q1, q2);
        Assert.AreEqual(7.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_NullOperand()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        
        Assert.Throws<NullReferenceException>(()=>Quantity<LengthUnit>.Subtract(q1, null!));
    }

    

   
    [TestMethod]
    public void TestDivision_SameUnit_FeetDividedByFeet()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        double ratio = Quantity<LengthUnit>.Divide(q1, q2);
        Assert.AreEqual(5.0, ratio, Epsilon);
    }

    [TestMethod]
    public void TestDivision_CrossUnit_FeetDividedByInches()
    {
        var q1 = new Quantity<LengthUnit>(24.0, LengthUnit.INCH);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        double ratio = Quantity<LengthUnit>.Divide(q1, q2);
        Assert.AreEqual(1.0, ratio, Epsilon);
    }

    [TestMethod]
    public void TestDivision_RatioLessThanOne()
    {
        var q1 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        double ratio = Quantity<LengthUnit>.Divide(q1, q2);
        Assert.AreEqual(0.5, ratio, Epsilon);
    }

    [TestMethod]
    public void TestDivision_ByZero()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.FEET);
        Assert.Throws<DivideByZeroException>(()=>Quantity<LengthUnit>.Divide(q1, q2));
    }

    [TestMethod]
    public void TestDivision_CrossUnit_KilogramDividedByGram()
    {
        var q1 = new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM);
        var q2 = new Quantity<WeightUnit>(2000.0, WeightUnit.GRAM);
        double ratio = Quantity<WeightUnit>.Divide(q1, q2);
        Assert.AreEqual(1.0, ratio, Epsilon);
    }


    [TestMethod]
    public void TestSubtractionAddition_Inverse()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        
        var added = Quantity<LengthUnit>.Add(q1, q2);
        var result = Quantity<LengthUnit>.Subtract(added, q2);
        
        Assert.AreEqual(10.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestSubtraction_Immutability()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        
        Quantity<LengthUnit>.Subtract(q1, q2);
        
        // Value of q1 should not change
        Assert.AreEqual(10.0, q1.Value);
    }

    [TestMethod]
    public void TestSubtractionAndDivision_Integration()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        var q3 = new Quantity<LengthUnit>(4.0, LengthUnit.FEET);
        
        // (10 - 2) / 4 = 2.0
        var diff = Quantity<LengthUnit>.Subtract(q1, q2);
        double ratio = Quantity<LengthUnit>.Divide(diff, q3);
        
        Assert.AreEqual(2.0, ratio, Epsilon);
    }


}