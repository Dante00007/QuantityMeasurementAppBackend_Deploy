namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.generic;

[TestClass]
public class QuantityGenericVolumeTest
{
    private const double Epsilon = 1e-4;

    #region Equality Tests
    [TestMethod]
    public void TestEquality_LitreToLitre_SameValue()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_LitreToLitre_DifferentValue()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
        Assert.IsFalse(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_LitreToMillilitre_EquivalentValue()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_MillilitreToLitre_EquivalentValue()
    {
        var q1 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
        var q2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_LitreToGallon_EquivalentValue()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(0.264172, VolumeUnit.GALLON);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_GallonToLitre_EquivalentValue()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON);
        var q2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);
        Assert.IsTrue(q1.Equals(q2));
    }

    [TestMethod]
    public void TestEquality_SameReference()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        Assert.IsTrue(q1.Equals(q1));
    }

    [TestMethod]
    public void TestEquality_NullComparison()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        Assert.IsFalse(q1.Equals(null));
    }

    [TestMethod]
    public void TestEquality_ZeroValue()
    {
        var q1 = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.MILLILITRE);
        Assert.IsTrue(q1.Equals(q2));
    }
    #endregion

    #region Conversion Tests
    [TestMethod]
    public void TestConversion_LitreToMillilitre()
    {
        double result = Quantity<VolumeUnit>.Convert(1.0, VolumeUnit.LITRE, VolumeUnit.MILLILITRE);
        Assert.AreEqual(1000.0, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_GallonToLitre()
    {
        double result = Quantity<VolumeUnit>.Convert(1.0, VolumeUnit.GALLON, VolumeUnit.LITRE);
        Assert.AreEqual(3.78541, result, Epsilon);
    }

    [TestMethod]
    public void TestConversion_SameUnit()
    {
        double result = Quantity<VolumeUnit>.Convert(5.0, VolumeUnit.LITRE, VolumeUnit.LITRE);
        Assert.AreEqual(5.0, result, Epsilon);
    }
    #endregion

    #region Addition Tests
    [TestMethod]
    public void TestAddition_SameUnit_LitrePlusLitre()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE);
        var result = Quantity<VolumeUnit>.Add(q1, q2);
        Assert.AreEqual(3.0, result.Value, Epsilon);
        Assert.AreEqual(VolumeUnit.LITRE, result.Unit);
    }

    [TestMethod]
    public void TestAddition_CrossUnit_LitrePlusMillilitre()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);
        var result = Quantity<VolumeUnit>.Add(q1, q2);
        Assert.AreEqual(2.0, result.Value, Epsilon);
        Assert.AreEqual(VolumeUnit.LITRE, result.Unit);
    }

    [TestMethod]
    public void TestAddition_ExplicitTargetUnit_Gallon()
    {
        var q1 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);
        var result = Quantity<VolumeUnit>.Add(q1, q2, VolumeUnit.GALLON);
        Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_Commutativity()
    {
        var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE);

        var res1 = Quantity<VolumeUnit>.Add(q1, q2, VolumeUnit.LITRE);
        var res2 = Quantity<VolumeUnit>.Add(q2, q1, VolumeUnit.LITRE);

        Assert.AreEqual(res1.Value, res2.Value, Epsilon);
    }

    [TestMethod]
    public void TestAddition_NegativeValues()
    {
        var q1 = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE);
        var q2 = new Quantity<VolumeUnit>(-2000.0, VolumeUnit.MILLILITRE);
        var result = Quantity<VolumeUnit>.Add(q1, q2, VolumeUnit.LITRE);
        Assert.AreEqual(3.0, result.Value, Epsilon);
    }
    #endregion

    #region Enum Logic Tests
    [TestMethod]
    public void TestVolumeUnitEnum_LitreConstant()
    {
        Assert.AreEqual(1.0, VolumeUnit.LITRE.GetFactor());
    }

    [TestMethod]
    public void TestConvertToBaseUnit_MillilitreToLitre()
    {
        Assert.AreEqual(1.0, VolumeUnit.MILLILITRE.ConvertToBaseUnit(1000.0), Epsilon);
    }

    [TestMethod]
    public void TestConvertFromBaseUnit_LitreToGallon()
    {
        Assert.AreEqual(1.0, VolumeUnit.GALLON.ConvertFromBaseUnit(3.78541), Epsilon);
    }
    #endregion
}