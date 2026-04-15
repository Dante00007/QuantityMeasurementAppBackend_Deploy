using QuantityMeasurementAppConsole.Controllers;
using QuantityMeasurementAppModelLayer.Units;
// using QuantityMeasurementAppModelLayer.Core;
using QuantityMeasurementAppModelLayer.Entity;

namespace QuantityMeasurementAppConsole.Menu;

public class Menu
{
    private readonly MeasurementController _controller = new();
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Quantity Measurement Converter ===");
            Console.WriteLine("1. Length (Feet, Inch, Yard, CM)");
            Console.WriteLine("2. Weight (KG, Gram, Pound)");
            Console.WriteLine("3. Volume (Litre, ML, Gallon)");
            Console.WriteLine("4. Temperature (Celsius, Fahrenheit)");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect Category: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            try
            {
                switch (choice)
                {
                    case "1": RunCategoryMenu<LengthUnit>(1); break;
                    case "2": RunCategoryMenu<WeightUnit>(2); break;
                    case "3": RunCategoryMenu<VolumeUnit>(3); break;
                    case "4": RunCategoryMenu<TemperatureUnit>(4); break;
                    default: Console.WriteLine("Invalid Selection."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to Main Menu...");
            Console.ReadKey();
        }

        void RunCategoryMenu<U>(int enumIndex) where U : Enum
        {
            Console.Clear();
            Console.WriteLine($"--- {typeof(U).Name} Operations ---");


            var units = (U[])Enum.GetValues(typeof(U));
            for (int i = 0; i < units.Length; i++)
                Console.WriteLine($"{i}. {units[i]}");

            Console.Write("\nSelect Input Unit (index): ");
            U unit1 = units[int.Parse(Console.ReadLine())];
            Console.Write("Enter Value: ");
            double val1 = double.Parse(Console.ReadLine());

            QuantityDTO q1 = new QuantityDTO(val1, unit1.ToString(), enumIndex);

            Console.WriteLine("\n1. Convert");
            Console.WriteLine("2. Add");
            Console.WriteLine("3. Subtract");
            Console.Write("Action: ");
            string action = Console.ReadLine();
 
            if (action == "1")
            {
                Console.Write("Select Target Unit (index): ");
                U toUnit = units[int.Parse(Console.ReadLine())];
                _controller.HandleConversion(q1, toUnit.ToString());
            }
            else
            {
                Console.Write("Select Second Unit (index): ");
                U unit2 = units[int.Parse(Console.ReadLine())];
                Console.Write("Enter Second Value: ");
                double val2 = double.Parse(Console.ReadLine());
                QuantityDTO q2 = new QuantityDTO(val2, unit2.ToString(), enumIndex);

                if (action == "2")
                {
                    Console.Write("Select Target Unit (index): ");
                    U target = units[int.Parse(Console.ReadLine())];
                    _controller.HandleAddition(q1, q2, target.ToString());

                }
                else if (action == "3")
                {
                    Console.Write("Select Target Unit (index): ");
                    U target = units[int.Parse(Console.ReadLine())];
                    _controller.HandleSubtraction(q1, q2, target.ToString());

                }
            }
        }
    }
}