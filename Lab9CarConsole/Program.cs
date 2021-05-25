using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarLib;

namespace Lab9Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Lab №9";
            ProgramInfo();
            Console.WriteLine();
            Console.WriteLine("Let's set up your car");
            bool parsed;
            Console.WriteLine("Enter the max speed (km/h):");
            parsed = int.TryParse(Console.ReadLine(), out int maxSpeed);
            while (!parsed || maxSpeed <= 0)
            {
                Console.WriteLine("Error: you have entered not a number or it was too low for max speed");
                Console.WriteLine("Enter the max speed (km/h) once more:");
                parsed = int.TryParse(Console.ReadLine(), out maxSpeed);
            }
            Console.WriteLine("Enter the max tank capacity (litres):");
            parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out double maxTankCapacity);
            while (!parsed || maxTankCapacity <= 0)
            {
                Console.WriteLine("Error: you have entered not a number or it was too low for max tank capacity");
                Console.WriteLine("Enter the max tank capacity (litres) once more:");
                parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out maxTankCapacity);
            }
            Console.WriteLine("Enter the average fuel consumption (l/100km):");
            parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out double fuelConsumption);
            while (!parsed || fuelConsumption <= 0)
            {
                Console.WriteLine("Error: you have entered not a number or it was too low for fuel consumption");
                Console.WriteLine("Enter the average fuel consumption (l/100km) once more:");
                parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out fuelConsumption);
            }
            Console.WriteLine("Enter the brand name of your car:");
            string brandName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(brandName))
            {
                Console.WriteLine("Brand name cannot be empty or contain only whitespaces");
                Console.WriteLine("Enter the brand name of your car once more:");
                brandName = Console.ReadLine();
            }
            Car car = new Car(maxSpeed, maxTankCapacity, fuelConsumption, brandName);
            car.MaxMileageExceeded += MileageEventsHandler;
            CarMenu(car);
        }
        static void ProgramInfo()                                       // prints information about the program
        {
            Console.WriteLine("Lab №9. Nikita Chernikov, IS-02");
            Console.WriteLine("Researching of pointers to functions C++ and delegates C#");
            Console.WriteLine("Variant 15");
        }
        static void CarMenu(Car car)                                    // car menu
        {
            PrintHelp(car);
            bool parsed;
            short choice;
            const short minPoint = 1;
            const short maxPoint = 4;
            do
            {
                Console.WriteLine($"\nEnter the number {minPoint} - {maxPoint}: ");
                parsed = short.TryParse(Console.ReadLine(), out choice);
                if (!parsed || choice < minPoint || choice > maxPoint)
                    choice = minPoint - 1;
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        PrintCarState(car);
                        Console.WriteLine();
                        if (car.IsStarted)
                        {
                            car.Stop();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The engine was stopped successfully");
                            Console.ResetColor();
                        }
                        else
                        {
                            try
                            {
                                car.Start();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The engine was started successfully");
                                Console.ResetColor();
                            }
                            catch(InvalidOperationException exc)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(exc.Message);
                                Console.ResetColor();
                            }
                        }
                        Console.WriteLine("\nPress ENTER to continue");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        PrintCarState(car);
                        Console.WriteLine();
                        double fuelCapacity;
                        Console.WriteLine("Enter how much petrol do you want to fuel (l):");
                        parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out fuelCapacity);
                        while (!parsed || fuelCapacity < 0 || car.TankCapacity + fuelCapacity > car.MaxTankCapacity)
                        {
                            Console.WriteLine("Error: you have entered not a number or it was negative,");
                            Console.WriteLine($"Or it was bigger than max tank capacity ({car.MaxTankCapacity:F1} l)");
                            Console.WriteLine("Enter how much petrol do you want to fuel (l) once more:");
                            parsed = double.TryParse(Console.ReadLine().Replace('.', ','), out fuelCapacity);
                        }
                        car.Refuel(fuelCapacity);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Refuelled successfully!");
                        Console.WriteLine($"Fuel tank now: {car.TankCapacity:F1}/{car.MaxTankCapacity:F1} l");
                        Console.ResetColor();
                        Console.WriteLine("\nPress ENTER to continue");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        PrintCarState(car);
                        Console.WriteLine();
                        if (car.IsStarted)
                        {
                            int speed, minutes;
                            Console.WriteLine("Enter the average speed (km/h):");
                            parsed = int.TryParse(Console.ReadLine(), out speed);
                            while (!parsed || speed <= 0 || speed > car.MaxSpeed)
                            {
                                Console.WriteLine("Error: you have entered not a number or it was smaller than or equal zero or it was higher than max speed");
                                Console.WriteLine("Enter the speed (km/h) once more:");
                                parsed = int.TryParse(Console.ReadLine(), out speed);
                            }
                            Console.WriteLine("\nEnter how many minutes you were in travel:");
                            parsed = int.TryParse(Console.ReadLine(), out minutes);
                            while (!parsed || minutes <= 0)
                            {
                                Console.WriteLine("Error: you have entered not a number or it was smaller than or equal zero");
                                Console.WriteLine("Enter how many minutes you were in travel once more:");
                                parsed = int.TryParse(Console.ReadLine(), out minutes);
                            }
                            Console.WriteLine();
                            try
                            {
                                double distance = car.Drive(speed, minutes);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"You successfully travelled {distance:F1} km");
                                Console.WriteLine("Your car state now:");
                                if (car.IsStarted)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ResetColor();
                                Console.Write("Engine");
                                Console.ResetColor();
                                Console.WriteLine($"\t\t\t\tFuel tank: {car.TankCapacity:F1}/{car.MaxTankCapacity:F1} l");
                                Console.WriteLine($"Mileage: {car.Mileage:000000} km");
                                Console.ResetColor();
                            }
                            catch (InvalidOperationException exc)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(exc.Message);
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please, start the engine first");
                            Console.ResetColor();
                        }
                        Console.WriteLine("\nPress ENTER to continue");
                        Console.ReadLine();
                        break;
                }
                PrintHelp(car);
            } while (choice != maxPoint);
        }
        public static void PrintHelp(Car car)                  // help (for the menu)
        {
            PrintCarState(car);
            Console.WriteLine("\nWhat do you want to do?");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("1. Start/stop the engine");
            Console.WriteLine("2. Refuel");
            Console.WriteLine("3. Drive");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("4. Quit");
            Console.ResetColor();
        }
        public static void PrintCarState(Car car)               // car state
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t\t\tCar \"{car.BrandName}\"");
            Console.ResetColor();
            if (car.IsStarted)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Engine");
            Console.ResetColor();
            Console.WriteLine($"\t\t\t\tFuel tank: {car.TankCapacity:F1}/{car.MaxTankCapacity:F1} l");
            Console.WriteLine($"Mileage: {car.Mileage:000000} km");
        }
        public static void MileageEventsHandler(object sender, CarEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You exceeded the max mileage!");
            Console.WriteLine($"Mileage was: {e.PreviousMileage} km");
            Console.WriteLine($"Distance: {e.Distance:F1} km");
            Console.WriteLine($"Max mileage: {e.MaxMileage} km");
            Console.WriteLine($"Car: {(sender as Car).BrandName}");
            Console.ResetColor();
        }
    }
}
