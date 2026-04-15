namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.models;

[TestClass]
public class QuantityWeightAllTests
{
    private const double Epsilon = 1e-4;

    [TestMethod]
    public void TestEquality_KilogramToKilogram_SameValue()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_KilogramToKilogram_DifferentValue()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
        Assert.IsFalse(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_KilogramToGram_EquivalentValue()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_GramToKilogram_EquivalentValue()
    {
        var q1 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var q2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_WeightVsLength_Incompatible()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsFalse(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_NullComparison()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        Assert.IsFalse(q1.Equals(null));
    }

    [TestMethod]
    public void TestEquality_SameReference()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        Assert.IsTrue(q1.Equals(q1));
    }

    [TestMethod]
    public void TestEquality_NullUnit()
    {
        Assert.Throws<ArgumentException>(() => new QuantityWeight(1.0, (WeightUnit)999));
    }

    [TestMethod]
    public void TestEquality_TransitiveProperty()
    {
        var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var b = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var c = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        Assert.IsTrue(a.Equals(b));
        Assert.IsTrue(b.Equals(c));
        Assert.IsTrue(a.Equals(c));
    }

    [TestMethod]
    public void TestEquality_ZeroValue()
    {
        var q1 = new QuantityWeight(0.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(0.0, WeightUnit.GRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_NegativeWeight()
    {
        var q1 = new QuantityWeight(-1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(-1000.0, WeightUnit.GRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_LargeWeightValue()
    {
        var q1 = new QuantityWeight(1000000.0, WeightUnit.GRAM);
        var q2 = new QuantityWeight(1000.0, WeightUnit.KILOGRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_SmallWeightValue()
    {
        var q1 = new QuantityWeight(0.001, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1.0, WeightUnit.GRAM);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestConversion_PoundToKilogram()
    {
        double result = QuantityWeight.Convert(2.20462, WeightUnit.POUND, WeightUnit.KILOGRAM);
        Assert.AreEqual(1.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_KilogramToPound()
    {
        double result = QuantityWeight.Convert(1.0, WeightUnit.KILOGRAM, WeightUnit.POUND);
        Assert.AreEqual(2.20462, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_SameUnit()
    {
        double result = QuantityWeight.Convert(5.0, WeightUnit.KILOGRAM, WeightUnit.KILOGRAM);
        Assert.AreEqual(5.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_ZeroValue()
    {
        double result = QuantityWeight.Convert(0.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
        Assert.AreEqual(0.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_NegativeValue()
    {
        double result = QuantityWeight.Convert(-1.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
        Assert.AreEqual(-1000.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_RoundTrip()
    {
        double start = 1.5;
        double toGram = QuantityWeight.Convert(start, WeightUnit.KILOGRAM, WeightUnit.GRAM);
        double backToKg = QuantityWeight.Convert(toGram, WeightUnit.GRAM, WeightUnit.KILOGRAM);
        Assert.AreEqual(start, backToKg, Epsilon);
    }

    [TestMethod]
    public void TestAddition_SameUnit_KilogramPlusKilogram()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.KILOGRAM);
        Assert.AreEqual(3.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_CrossUnit_KilogramPlusGram()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var result = QuantityWeight.Add(q1, q2);
        Assert.AreEqual(2.0, result.Value, Epsilon);
        Assert.AreEqual(WeightUnit.KILOGRAM, result.Unit);
    }

    [TestMethod]
    public void TestAddition_CrossUnit_PoundPlusKilogram()
    {
        var q1 = new QuantityWeight(2.20462, WeightUnit.POUND);
        var q2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.POUND);
        Assert.AreEqual(4.40924, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Kilogram()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.GRAM);
        Assert.AreEqual(2000.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_Commutativity()
    {
        var q1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var res1 = QuantityWeight.Add(q1, q2, WeightUnit.KILOGRAM);
        var res2 = QuantityWeight.Add(q2, q1, WeightUnit.KILOGRAM);
        Assert.AreEqual(res1.Value, res2.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_WithZero()
    {
        var q1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(0.0, WeightUnit.GRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.KILOGRAM);
        Assert.AreEqual(5.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_NegativeValues()
    {
        var q1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(-2000.0, WeightUnit.GRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.KILOGRAM);
        Assert.AreEqual(3.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_LargeValues()
    {
        var q1 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);
        var q2 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);
        var result = QuantityWeight.Add(q1, q2, WeightUnit.KILOGRAM);
        Assert.AreEqual(2e6, result.Value, Epsilon);
    }
}