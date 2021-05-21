using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class PrintHandler
    {
        public void IllegalOptionOutput()
        {
            Console.WriteLine("Error: Illegal option entered. Please try again.");
        }

        public void PrintList<T>(List<T> i_List)
        {
            foreach (T item in i_List)
            {
                Console.WriteLine(item);
            }
        }
        public void PrintVehicleDetails(StringBuilder i_Details)
        {
            Console.WriteLine(i_Details);
        }
        public void AskForLicenseNumber()
        {
            Console.WriteLine("Please enter license number: ");
        }

        public void AskForVehicleStatus()
        {
            Console.WriteLine("Choose the vehicle's new status: ");
        }

        public void AskToChooseFuelType()
        {
            Console.WriteLine("Please choose the desired fuel type: ");
        }

        public void AskForFuelingAmount()
        {
            Console.WriteLine("Please enter the desired fuel amount: ");
        }

        public void AskToChooseVehicleType()
        {
            Console.WriteLine("Please choose the desired vehicle type:");
        }

        public void AskForColor()
        {
            Console.WriteLine("Choose the car's color:");
        }

        public void AskForMotorcycleLicenseType()
        {
            Console.WriteLine("Choose license type:");
        }

        public void VehicleIsAlreadyExistsInGarage()
        {
            Console.WriteLine("Cannot add the desired Vehicle, as it's already exists in the garage. Current status is: in repair.");
        }

        public void AskForCurrentFuelAmount()
        {
            Console.WriteLine("Enter current fuel amount: ");
        }

        public void AskForWheelManufacturer()
        {
            Console.WriteLine("Enter wheels manufacturer:" + Environment.NewLine);
        }

        public void AskForCurrentBatteryTime()
        {
            Console.WriteLine("Enter current battery time remaining: ");
        }

        public void AskForMaxCarryingWeight()
        {
            Console.WriteLine("Enter maximum carrying weight: ");
        }

        public void AskIfTruckIsCarryingDangerousCargo()
        {
            Console.WriteLine("Type 'y' if truck is carrying dangerous cargo, else type 'n'");
        }

        public void AskForVehicleModel()
        {
            Console.WriteLine("Enter vehicle model: ");
        }

        public void AskForCurrentWheelAirPressure()
        {
            Console.WriteLine("Enter current air pressure of car's wheels: ");
        }

        public void AskForEngineVolume()
        {
            Console.WriteLine("Enter engine volume: ");
        }

        public void AskForOwnerName()
        {
            Console.WriteLine("Enter your name: ");
        }

        public void AskForOwnerPhone()
        {
            Console.WriteLine("Enter your phone number (8 to 10 digits): ");
        }

        public void PrintException(Exception ex, string msg)
        {
            Console.WriteLine($"{msg}{Environment.NewLine}");
        }


        public void AskForAmountOfBatteryInMinutesToCharge()
        {
            Console.WriteLine("Enter the amount of battery minutes you want to charge: " + Environment.NewLine);
        }

        public void NoMatchingVehiclesInGarage()
        {
            Console.WriteLine("No matching vehicles in garage" + Environment.NewLine);
        }

        public void AskForNumberOfDoors()
        {
            Console.WriteLine("Enter number of doors (2 to 5): ");
        }
    }
}