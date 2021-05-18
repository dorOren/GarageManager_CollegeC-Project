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
                Console.WriteLine(item.ToString());
            }
        }

        public static void AskForLicenseNumber()
        {
            Console.WriteLine("Please enter license number.");
        }

        public static void AskToChooseFuelType()
        {
            Console.WriteLine("Please enter the desired fuel type");
        }

        public static void AskForFuelingAmount()
        {
            Console.WriteLine("Please enter the desired fuel amount");
        }


        public static void AskToChooseVehicleType()
        {
            Console.WriteLine("Please choose the desired vehicle type");
        }

        public static void AskForColor()
        {
            Console.WriteLine("Choose the car's color");
        }
    }
}