namespace QuantityMeasurementApp.Tests;


using QuantityMeasurementApp.models;


[TestClass]
public class QuantityLengthAdditionExplicitTests
{
    private const double Epsilon = 1e-4;

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Feet()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var expected = new QuantityLength(2.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2, LengthUnit.FEET));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Inches()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var expected = new QuantityLength(24.0, LengthUnit.INCH);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Yards()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.YARD);
        
        Assert.AreEqual(0.6667, result.Value, Epsilon);
        Assert.AreEqual(LengthUnit.YARD, result.Unit);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Centimeters()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.INCH);
        var q2 = new QuantityLength(1.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.CENTIMETER);
        
        Assert.AreEqual(5.08, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_SameAsFirstOperand()
    {
        var q1 = new QuantityLength(2.0, LengthUnit.YARD);
        var q2 = new QuantityLength(3.0, LengthUnit.FEET);
        var expected = new QuantityLength(3.0, LengthUnit.YARD);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_SameAsSecondOperand()
    {
        var q1 = new QuantityLength(2.0, LengthUnit.YARD);
        var q2 = new QuantityLength(3.0, LengthUnit.FEET);
        var expected = new QuantityLength(9.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2, LengthUnit.FEET));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Commutativity()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        
        var result1 = QuantityLength.Add(q1, q2, LengthUnit.YARD);
        var result2 = QuantityLength.Add(q2, q1, LengthUnit.YARD);
        
        Assert.AreEqual(result1, result2);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_WithZero()
    {
        var q1 = new QuantityLength(5.0, LengthUnit.FEET);
        var q2 = new QuantityLength(0.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.YARD);
        
        Assert.AreEqual(1.6667, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_NegativeValues()
    {
        var q1 = new QuantityLength(5.0, LengthUnit.FEET);
        var q2 = new QuantityLength(-2.0, LengthUnit.FEET);
        var expected = new QuantityLength(36.0, LengthUnit.INCH);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_LargeToSmallScale()
    {
        var q1 = new QuantityLength(1000.0, LengthUnit.FEET);
        var q2 = new QuantityLength(500.0, LengthUnit.FEET);
        var expected = new QuantityLength(18000.0, LengthUnit.INCH);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2, LengthUnit.INCH));
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_SmallToLargeScale()
    {
        var q1 = new QuantityLength(12.0, LengthUnit.INCH);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.YARD);
        
     
        Assert.AreEqual(0.6667, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_PrecisionTolerance()
    {
        var q1 = new QuantityLength(0.1, LengthUnit.INCH);
        var q2 = new QuantityLength(0.2, LengthUnit.INCH);
        var result = QuantityLength.Add(q1, q2, LengthUnit.CENTIMETER);
   
        Assert.AreEqual(0.762, result.Value, Epsilon);
    }
}