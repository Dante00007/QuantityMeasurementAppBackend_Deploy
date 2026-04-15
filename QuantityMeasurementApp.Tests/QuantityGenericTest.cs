namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.generic;

[TestClass]
public class QuantityGenericTest
{
    private const double Epsilon = 1e-6;

    #region 1. Interface and Implementation Tests

    [TestMethod]
    public void TestIMeasurableInterface_LengthUnitImplementation()
    {
        // Verifies that LengthUnit can be used with conversion logic (Interface contract)
        LengthUnit unit = LengthUnit.FEET;
        Assert.AreEqual(1.0, unit.GetFactor());
        Assert.AreEqual(12.0, unit.ConvertToBaseUnit(12.0)); // 12 Feet = 12 Base Units
        Assert.AreEqual(1.0, unit.ConvertFromBaseUnit(1.0));
    }

    [TestMethod]
    public void TestIMeasurableInterface_WeightUnitImplementation()
    {
        WeightUnit unit = WeightUnit.GRAM;
        Assert.AreEqual(0.001, unit.GetFactor());
        Assert.AreEqual(1.0, unit.ConvertToBaseUnit(1000.0)); // 1000g = 1kg
    }

    #endregion

    #region 2. Equality and Logic Tests

    [TestMethod]
    public void TestGenericQuantity_LengthOperations_Equality()
    {
        var feet = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var inches = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);
        Assert.IsTrue(feet.Equals(inches));
    }

    [TestMethod]
    public void TestGenericQuantity_WeightOperations_Equality()
    {
        var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);
        var grams = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM);
        Assert.IsTrue(kg.Equals(grams));
    }

    [TestMethod]
    public void TestGenericQuantity_LengthOperations_Conversion()
    {
        var feet = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        double result = Quantity<LengthUnit>.Convert(feet.Value, feet.Unit, LengthUnit.INCH);
        Assert.AreEqual(12.0, result, Epsilon);
    }

    #endregion

    #region 3. Addition Tests

    [TestMethod]
    public void TestGenericQuantity_LengthOperations_Addition()
    {
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);
        // Returns 2.0 Feet
        var result = Quantity<LengthUnit>.Add(q1, q2);

        Assert.AreEqual(2.0, result.Value, Epsilon);
        Assert.AreEqual(LengthUnit.FEET, result.Unit);
    }

    [TestMethod]
    public void TestGenericQuantity_WeightOperations_Addition()
    {
        var q1 = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);
        var q2 = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM);
        // result in KG
        var result = Quantity<WeightUnit>.Add(q1, q2, WeightUnit.KILOGRAM);

        Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    #endregion

    #region 4. Validation and Safety Tests

    [TestMethod]
    public void TestCrossCategoryPrevention_LengthVsWeight()
    {
        var feet = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM);

        // Equals should return false because they are different types
        Assert.IsFalse(feet.Equals(kg));
    }

    [TestMethod]
    public void TestGenericQuantity_ConstructorValidation_InvalidValue()
    {
        Assert.Throws<ArgumentException>(() => new Quantity<LengthUnit>(double.NaN, LengthUnit.FEET));
    }

    [TestMethod]
    public void TestImmutability_GenericQuantity()
    {
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

        Quantity<LengthUnit>.Add(q1, q2);

        // Original object should remain unchanged
        Assert.AreEqual(1.0, q1.Value);
    }

    #endregion

    #region 5. Scalability Tests

    [TestMethod]
    public void TestScalability_NewUnitEnumIntegration()
    {

        Assert.Inconclusive("Requires VolumeUnit Enum to be defined in models.");
    }

    [TestMethod]
    public void TestHashCode_GenericQuantity_Consistency()
    {
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);

        Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
    }

    #endregion
}