namespace QuantityMeasurementApp.Tests;


using QuantityMeasurementApp.models;


[TestClass]
public class QuantityLengthAdditionTests
{
    private const double Epsilon = 1e-6;
   [TestMethod]
    public void TestAddition_SameUnit_FeetPlusFeet()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(2.0, LengthUnit.FEET);
        var expected = new QuantityLength(3.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_SameUnit_InchPlusInch()
    {
        var q1 = new QuantityLength(6.0, LengthUnit.INCH);
        var q2 = new QuantityLength(6.0, LengthUnit.INCH);
        var expected = new QuantityLength(12.0, LengthUnit.INCH);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_CrossUnit_FeetPlusInches()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var expected = new QuantityLength(2.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_CrossUnit_InchPlusFeet()
    {
        var q1 = new QuantityLength(12.0, LengthUnit.INCH);
        var q2 = new QuantityLength(1.0, LengthUnit.FEET);
        var expected = new QuantityLength(24.0, LengthUnit.INCH);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_CrossUnit_YardPlusFeet()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.YARD);
        var q2 = new QuantityLength(3.0, LengthUnit.FEET);
        var expected = new QuantityLength(2.0, LengthUnit.YARD);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_CrossUnit_CentimeterPlusInch()
    {
        var q1 = new QuantityLength(2.54, LengthUnit.CENTIMETER);
        var q2 = new QuantityLength(1.0, LengthUnit.INCH);
        var expected = new QuantityLength(5.08, LengthUnit.CENTIMETER);
        // Using Equals check because of precision snap to 5.08
        Assert.IsTrue(expected.Equals(QuantityLength.Add(q1, q2)));
    }

    [TestMethod]
    public void TestAddition_Commutativity()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        var q2 = new QuantityLength(12.0, LengthUnit.INCH);
        var result1 = QuantityLength.Add(q1, q2);
        var result2 = QuantityLength.Add(q2, q1);
        Assert.AreEqual(result1, result2);
    }

    [TestMethod]
    public void TestAddition_WithZero()
    {
        var q1 = new QuantityLength(5.0, LengthUnit.FEET);
        var q2 = new QuantityLength(0.0, LengthUnit.INCH);
        var expected = new QuantityLength(5.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_NegativeValues()
    {
        var q1 = new QuantityLength(5.0, LengthUnit.FEET);
        var q2 = new QuantityLength(-2.0, LengthUnit.FEET);
        var expected = new QuantityLength(3.0, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_NullSecondOperand()
    {
        var q1 = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.Throws<ArgumentException>(() => 
            QuantityLength.Add(q1, null!));
    }

    [TestMethod]
    public void TestAddition_LargeValues()
    {
        var q1 = new QuantityLength(1e6, LengthUnit.FEET);
        var q2 = new QuantityLength(1e6, LengthUnit.FEET);
        var expected = new QuantityLength(2e6, LengthUnit.FEET);
        Assert.AreEqual(expected, QuantityLength.Add(q1, q2));
    }

    [TestMethod]
    public void TestAddition_SmallValues()
    {
        var q1 = new QuantityLength(0.001, LengthUnit.FEET);
        var q2 = new QuantityLength(0.002, LengthUnit.FEET);
        var result = QuantityLength.Add(q1, q2);
        
        Assert.AreEqual(0.003, result.Value, Epsilon);
    }
}