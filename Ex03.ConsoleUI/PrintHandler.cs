using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class PrintHandler
    {
        public static void IllegalOptionOutput()
        {
            Console.WriteLine("Error: Illegal option entered. Please try again.");
        }

        public static void PrintList<T>(List<T> i_List)
        {
            foreach (T item in i_List)
            {
                Console.WriteLine(item);
            }
        }
        public static void PrintVehicleDetails(StringBuilder i_Details)
        {
            Console.WriteLine(i_Details);
        }
        public static void AskForLicenseNumber()
        {
            Console.WriteLine("Please enter license number: ");
        }

        public static void AskForVehicleStatus()
        {
            Console.WriteLine("Choose the vehicle's new status: ");
        }

        public static void AskToChooseFuelType()
        {
            Console.WriteLine("Please choose the desired fuel type: ");
        }

        public static void AskForFuelingAmount()
        {
            Console.WriteLine("Please enter the desired fuel amount: ");
        }


        public static void AskToChooseVehicleType()
        {
            Console.WriteLine("Please choose the desired vehicle type:");
        }

        public static void AskForColor()
        {
            Console.WriteLine("Choose the car's color:");
        }

        public static void AskForMotorcycleLicenseType()
        {
            Console.WriteLine("Choose license type:");
        }

        public static void VehicleIsAlreadyExistsInGarage()
        {
            Console.WriteLine("Cannot add the desired Vehicle, as it's already exists in the garage. Current status is: in repair.");
        }

        public static void AskForCurrentFuelAmount()
        {
            Console.WriteLine("Enter current fuel amount: ");
        }

        public static void AskForWheelManufacturer()
        {
            Console.WriteLine("Enter wheels manufacturer:" + Environment.NewLine);
        }

        public static void AskForCurrentBatteryTime()
        {
            Console.WriteLine("Enter current battery time remaining: ");
        }

        public static void AskForMaxCarryingWeight()
        {
            Console.WriteLine("Enter maximum carrying weight: ");
        }

        public static void AskIfTruckIsCarryingDangerousCargo()
        {
            Console.WriteLine("Type 'y' if truck is carrying dangerous cargo, else type 'n'");
        }

        public static void AskForVehicleModel()
        {
            Console.WriteLine("Enter vehicle model: ");
        }

        public static void AskForCurrentWheelAirPressure()
        {
            Console.WriteLine("Enter current air pressure of car's wheels: ");
        }

        public static void AskForEngineVolume()
        {
            Console.WriteLine("Enter engine volume: ");
        }

        public static void AskForOwnerName()
        {
            Console.WriteLine("Enter your name: ");
        }

        public static void AskForOwnerPhone()
        {
            Console.WriteLine("Enter your phone number: ");
        }

        public static void PrintException(Exception ex, string msg)
        {
            Console.WriteLine($"{msg}{Environment.NewLine}");
        }


        public static void AskForAmountOfBatteryInMinutesToCharge()
        {
            Console.WriteLine("Enter the amount of battery minutes you want to charge: " + Environment.NewLine);
        }

        public static void NoMatchingVehiclesInGarage()
        {
            Console.WriteLine("No matching vehicles in garage" + Environment.NewLine);
        }

        public static void AskForNumberOfDoors()
        {
            Console.WriteLine("Enter number of doors");
        }
    }
}