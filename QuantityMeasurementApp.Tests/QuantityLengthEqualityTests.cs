﻿namespace QuantityMeasurementApp.Tests;

using QuantityMeasurementApp.models;

[TestClass]
public class QuantityLengthEqualityTests
{
   
    [TestMethod]
    public void TestEquality_FeetToFeet_SameValue()
    {
        var feet1 = new QuantityLength(1.0, LengthUnit.FEET);
        var feet2 = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsTrue(feet1.Equals(feet2));
    }


    [TestMethod]
    public void TestEquality_InchToInch_SameValue()
    {
        var inch1 = new QuantityLength(1.0, LengthUnit.INCH);
        var inch2 = new QuantityLength(1.0, LengthUnit.INCH);
        Assert.IsTrue(inch1.Equals(inch2));
    }

    [TestMethod]
    public void TestEquality_FeetToInch_EquivalentValue()
    {
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        var inch = new QuantityLength(12.0, LengthUnit.INCH);
        Assert.IsTrue(feet.Equals(inch));
    }

 
    [TestMethod]
    public void TestEquality_InchToFeet_EquivalentValue()
    {
        var inch = new QuantityLength(12.0, LengthUnit.INCH);
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsTrue(inch.Equals(feet));
    }

    [TestMethod]
    public void TestEquality_FeetToFeet_DifferentValue()
    {
        var feet1 = new QuantityLength(1.0, LengthUnit.FEET);
        var feet2 = new QuantityLength(2.0, LengthUnit.FEET);
        Assert.IsFalse(feet1.Equals(feet2));
    }

   
    [TestMethod]
    public void TestEquality_InchToInch_DifferentValue()
    {
        var inch1 = new QuantityLength(1.0, LengthUnit.INCH);
        var inch2 = new QuantityLength(2.0, LengthUnit.INCH);
        Assert.IsFalse(inch1.Equals(inch2));
    }


    [TestMethod]
    public void TestEquality_SameReference()
    {
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsTrue(feet.Equals(feet));
    }


    [TestMethod]
    public void TestEquality_NullComparison()
    {
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsFalse(feet.Equals(null));
    }

    [TestMethod]
    public void TestEquality_InvalidType()
    {
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        string notAQuantity = "1.0 feet";
        Assert.IsFalse(feet.Equals(notAQuantity));
    }

    [TestMethod]
    public void TestEquality_YARDToYARD_SameValue()
    {
        var yard1 = new QuantityLength(1.0, LengthUnit.YARD);
        var yard2 = new QuantityLength(1.0, LengthUnit.YARD);
        Assert.IsTrue(yard1.Equals(yard2));
    }

    [TestMethod]
    public void TestEquality_YARDToYARD_DifferentValue()
    {
        var yard1 = new QuantityLength(1.0, LengthUnit.YARD);
        var yard2 = new QuantityLength(2.0, LengthUnit.YARD);
        Assert.IsFalse(yard1.Equals(yard2));
    }

    [TestMethod]
    public void TestEquality_YARDToFeet_EquivalentValue()
    {
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        var feet = new QuantityLength(3.0, LengthUnit.FEET);
        Assert.IsTrue(yard.Equals(feet));
    }

    [TestMethod]
    public void TestEquality_YARDToInch_EquivalentValue()
    {
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        var inch = new QuantityLength(36.0, LengthUnit.INCH);
        Assert.IsTrue(yard.Equals(inch));
    }

    [TestMethod]
    public void TestEquality_YARDToCentimeter_EquivalentValue()
    {
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        var centimeter = new QuantityLength(91.44, LengthUnit.CENTIMETER);
        Assert.IsTrue(yard.Equals(centimeter));
    }

    [TestMethod]
    public void TestEquality_CentimeterToCentimeter_SameValue()
    {
        var centimeter1 = new QuantityLength(1.0, LengthUnit.CENTIMETER);
        var centimeter2 = new QuantityLength(1.0, LengthUnit.CENTIMETER);
        Assert.IsTrue(centimeter1.Equals(centimeter2));
    }

    [TestMethod]
    public void TestEquality_FeetToYard_EquivalentValue()
    {
        var feet = new QuantityLength(3.0, LengthUnit.FEET);
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        Assert.IsTrue(feet.Equals(yard), "Symmetry check: 3 Feet should equal 1 Yard");
    }

    [TestMethod]
    public void TestEquality_InchesToYard_EquivalentValue()
    {
        var inch = new QuantityLength(36.0, LengthUnit.INCH);
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        Assert.IsTrue(inch.Equals(yard), "Symmetry check: 36 Inches should equal 1 Yard");
    }

    [TestMethod]
    public void TestEquality_YardToFeet_NonEquivalentValue()
    {
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        var feet = new QuantityLength(2.0, LengthUnit.FEET);
        Assert.IsFalse(yard.Equals(feet), "1 Yard should not equal 2 Feet");
    }

    [TestMethod]
    public void TestEquality_CentimetersToInches_EquivalentValue()
    {
        
        var cm = new QuantityLength(1.0, LengthUnit.CENTIMETER);
        var inch = new QuantityLength(0.393701, LengthUnit.INCH);
        Assert.IsTrue(cm.Equals(inch), "1 cm should be equivalent to 0.393701 inches");
    }

    [TestMethod]
    public void TestEquality_CentimetersToFeet_NonEquivalentValue()
    {
        var cm = new QuantityLength(1.0, LengthUnit.CENTIMETER);
        var feet = new QuantityLength(1.0, LengthUnit.FEET);
        Assert.IsFalse(cm.Equals(feet), "1 cm should not equal 1 Foot");
    }

    [TestMethod]
    public void TestEquality_MultiUnit_TransitiveProperty()
    {
        var a = new QuantityLength(1.0, LengthUnit.YARD);
        var b = new QuantityLength(3.0, LengthUnit.FEET);
        var c = new QuantityLength(36.0, LengthUnit.INCH);

        Assert.IsTrue(a.Equals(b), "A should equal B");
        Assert.IsTrue(b.Equals(c), "B should equal C");
        Assert.IsTrue(a.Equals(c), "Transitive Property: A should equal C");
    }

    [TestMethod]
    public void TestEquality_YardSameReference()
    {
        var yard = new QuantityLength(1.0, LengthUnit.YARD);
        Assert.IsTrue(yard.Equals(yard), "Yard object should equal itself");
    }

    [TestMethod]
    public void TestEquality_CentimetersSameReference()
    {
        var cm = new QuantityLength(1.0, LengthUnit.CENTIMETER);
        Assert.IsTrue(cm.Equals(cm), "Centimeter object should equal itself");
    }

    [TestMethod]
    public void TestEquality_AllUnits_ComplexScenario()
    {
        var yard = new QuantityLength(2.0, LengthUnit.YARD);
        var feet = new QuantityLength(6.0, LengthUnit.FEET);
        var inch = new QuantityLength(72.0, LengthUnit.INCH);

        Assert.IsTrue(yard.Equals(feet) && feet.Equals(inch), "2 Yards == 6 Feet == 72 Inches");
    }

}