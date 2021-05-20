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
            Console.WriteLine("Error: Illegal option entered. Please try again." + Environment.NewLine);
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
            Console.WriteLine(i_Details + Environment.NewLine);
        }
        public static void AskForLicenseNumber()
        {
            Console.WriteLine("Please enter license number: ");
        }

        public static void AskForVehicleStatus()
        {
            Console.WriteLine("Choose the vehicle's new status: " + Environment.NewLine);
        }

        public static void AskToChooseFuelType()
        {
            Console.WriteLine("Please enter the desired fuel type: ");
        }

        public static void AskForFuelingAmount()
        {
            Console.WriteLine("Please enter the desired fuel amount: ");
        }


        public static void AskToChooseVehicleType()
        {
            Console.WriteLine("Please choose the desired vehicle type" + Environment.NewLine);
        }

        public static void AskForColor()
        {
            Console.WriteLine("Choose the car's color" + Environment.NewLine);
        }

        public static void AskForMotorcycleLicenseType()
        {
            Console.WriteLine("Choose license type" + Environment.NewLine);
        }

        public static void VehicleIsAlreadyExistsInGarage()
        {
            Console.WriteLine("Cannot add the desired Vehicle, as it's already exists in the garage. Current status is: in repair." + Environment.NewLine);
        }

        public static void AskForCurrentFuelAmount()
        {
            Console.WriteLine("Enter current fuel amount: " + Environment.NewLine);
        }

        public static void AskForWheelManufacturer()
        {
            Console.WriteLine("Enter wheels manufacturer: + Environment.NewLine");
        }

        public static void AskForCurrentBatteryTime()
        {
            Console.WriteLine("Enter current battery time remaining: " + Environment.NewLine);
        }

        public static void AskIfTruckIsCarryingDangerousCargo()
        {
            Console.WriteLine("Type 'y' if truck is carrying dangerous cargo, else type 'n'" + Environment.NewLine);
        }

        public static void AskForOwnerName()
        {
            Console.WriteLine("Enter your name: " + Environment.NewLine);
        }

        public static void AskForOwnerPhone()
        {
            Console.WriteLine("Enter your phone number: " + Environment.NewLine);
        }

        public static void PrintException(Exception ex, string msg)
        {
            Console.WriteLine(msg+Environment.NewLine);
        }
    }
}