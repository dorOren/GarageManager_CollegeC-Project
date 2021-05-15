using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class Menu
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine("=============================================================");
            Console.WriteLine("1. Log new vehicle to the garage");
            Console.WriteLine("2. Show all license numbers of vehicle currently in the garage"); // done
            Console.WriteLine("3. Update vehicle state"); // done
            Console.WriteLine("4. Inflate wheels (to maximum)"); // done
            Console.WriteLine("5. Fuel vehicle");
            Console.WriteLine("6. Charge a vehicle");
            Console.WriteLine("7. Show full vehicle data");
            Console.WriteLine("8. Exit garage system");
            Console.WriteLine("=============================================================");
        }

        public static void ShowFilteringOptionsByVehicleStatusMenu()
        {
            Console.WriteLine("=============================================================");
            Console.WriteLine("1. Show in status 'Paid'"); // done
            Console.WriteLine("2. Show in status 'In Repair'"); // done
            Console.WriteLine("3. Show in status 'Repaired'"); // done
            Console.WriteLine("4. Show all"); // done
            Console.WriteLine("5. Go back"); // done
            Console.WriteLine("=============================================================");
        }

        public static void ShowVehicleStatusOptions()
        {//TODO: Convert to eStatus.XX.ToString()
            Console.WriteLine("=============================================================");
            Console.WriteLine("1. Paid"); // done
            Console.WriteLine("2. In Repair"); // done
            Console.WriteLine("3. Repaired"); // done
            Console.WriteLine("4. Go back"); // done
            Console.WriteLine("=============================================================");
        }

        public static void ShowFuelTypes()
        {// TODO: Convert to eFuelTypes.XX.ToString()
            Console.WriteLine("1. Soler");
            Console.WriteLine("2. Octan 95");
            Console.WriteLine("3. Octan 96");
            Console.WriteLine("4. Octan 98");
            Console.WriteLine("5. Go back");
        }
    }
}