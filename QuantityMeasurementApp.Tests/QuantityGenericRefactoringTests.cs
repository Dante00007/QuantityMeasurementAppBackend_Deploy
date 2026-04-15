namespace QuantityMeasurementApp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.generic;

[TestClass]
public class QuantityGenericRefactoringTests
{
    private const double Epsilon = 1e-4;

  
    [TestMethod]
    public void TestValidation_NullOperand_ConsistentAcrossOperations()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        
        var ex1 = Assert.Throws<ArgumentException>(() => q1.Add(null!));
        var ex2 = Assert.Throws<ArgumentException>(() => q1.Subtract(null!));
        var ex3 = Assert.Throws<ArgumentException>(() => q1.Divide(null!));

        Assert.AreEqual("Operand cannot be null", ex1.Message);
        Assert.AreEqual(ex1.Message, ex2.Message);
        Assert.AreEqual(ex1.Message, ex3.Message);
    }

    [TestMethod]
    public void TestValidation_NullTargetUnit_AddSubtractReject()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);

        var exAdd = Assert.Throws<ArgumentException>(() => q1.Add(q2,default));
        var exSub = Assert.Throws<ArgumentException>(() => q1.Subtract(q2, default));

        Assert.AreEqual("Target unit cannot be null", exAdd.Message);
        Assert.AreEqual("Target unit cannot be null", exSub.Message);
    }

    [TestMethod]
    public void TestDivision_ByZero_ThrowsArithmeticException()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(0.0, LengthUnit.FEET);

        Assert.Throws<ArithmeticException>(() => q1.Divide(q2));
    }
  


    [TestMethod]
    public void TestRefactoring_Add_DelegatesViaHelper()
    {
        // Verifies that Add uses performBaseArithmetic to get 10+5 = 15
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        
        var result = q1.Add(q2);
        
        Assert.AreEqual(15.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestRefactoring_Subtract_DelegatesViaHelper()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        
        var result = q1.Subtract(q2);
        
        Assert.AreEqual(5.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestRefactoring_Divide_DelegatesViaHelper()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        
        double ratio = q1.Divide(q2);
        
        Assert.AreEqual(5.0, ratio, Epsilon);
    }
    
    [TestMethod]
    public void TestRounding_AddSubtract_TwoDecimalPlaces()
    {
        // Your ConvertFromBase uses Math.Round(result, 2)
        var q1 = new Quantity<LengthUnit>(1.234, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(0.001, LengthUnit.FEET);
        
        var result = q1.Add(q2); // 1.235 -> should round to 1.24 or 1.23 depending on midpoint
        
        // Asserting that the value has no more than 2 decimal places
        Assert.AreEqual(Math.Round(result.Value, 2), result.Value);
    }

    [TestMethod]
    public void TestRounding_Divide_NoRounding()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(3.0, LengthUnit.FEET);
        
        double ratio = q1.Divide(q2);
        
        // Division should return raw double (3.3333...) not rounded to 2 decimals
        Assert.AreNotEqual(3.33, ratio);
        Assert.IsTrue(Math.Abs(3.333333 - ratio) < 0.0001);
    }
    
    [TestMethod]
    public void TestImplicitTargetUnit_AddSubtract()
    {
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCH);
        
        var result = q1.Add(q2); // Should default to FEET (q1's unit)
        
        Assert.AreEqual(LengthUnit.FEET, result.Unit);
        Assert.AreEqual(2.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestExplicitTargetUnit_AddSubtract_Overrides()
    {
        var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
        
        var result = q1.Add(q2, LengthUnit.INCH);
        
        Assert.AreEqual(LengthUnit.INCH, result.Unit);
        Assert.AreEqual(24.0, result.Value, Epsilon);
    }

    [TestMethod]
    public void TestImmutability_AfterAdd_ViaCentralizedHelper()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET);
        
        q1.Add(q2);
        
        Assert.AreEqual(10.0, q1.Value, "Original object q1 must remain unchanged");
        Assert.AreEqual(5.0, q2.Value, "Original object q2 must remain unchanged");
    }

    [TestMethod]
    public void TestArithmetic_Chain_Operations()
    {
        var q1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
        var q2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);
        var q3 = new Quantity<LengthUnit>(4.0, LengthUnit.FEET);
        
        // (10 + 2) - 4 = 8.0
        var result = q1.Add(q2).Subtract(q3);
        
        Assert.AreEqual(8.0, result.Value, Epsilon);
    }
    
    [TestMethod]
    public void TestAllOperations_AcrossAllCategories()
    {
        // Weight
        var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
        Assert.AreEqual(0.5, w1.Subtract(new Quantity<WeightUnit>(500, WeightUnit.GRAM)).Value);

        // Length
        var l1 = new Quantity<LengthUnit>(1, LengthUnit.FEET);
        Assert.AreEqual(1.5, l1.Add(new Quantity<LengthUnit>(6, LengthUnit.INCH)).Value);

        // Volume
        var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
        Assert.AreEqual(2.0, v1.Divide(new Quantity<VolumeUnit>(500, VolumeUnit.MILLILITRE)));
    }
}