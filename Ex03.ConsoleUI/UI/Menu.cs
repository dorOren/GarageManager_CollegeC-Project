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

            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Log new vehicle to the garage").AppendLine()
                .Append("2. Show all license numbers of vehicle currently in the garage").AppendLine()
                .Append("3. Update vehicle state").AppendLine()
                .Append("4. Inflate wheels (to maximum)").AppendLine()
                .Append("5. Fuel vehicle").AppendLine()
                .Append("6. Charge a vehicle").AppendLine()
                .Append("7. Show full vehicle data").AppendLine()
                .Append("8. Exit garage system").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowUpdatingOptionsByVehicleStatusMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Update to status 'Paid'").AppendLine()
                .Append("2. Update to status 'In Repair'").AppendLine()
                .Append("3. Update to status 'Repaired'").AppendLine()
                .Append("4. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowFilteringOptionsByVehicleStatusMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Show in status 'Paid'").AppendLine()
                .Append("2. Show in status 'In Repair'").AppendLine()
                .Append("3. Show in status 'Repaired'").AppendLine()
                .Append("4. Show all").AppendLine()
                .Append("5. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowVehicleStatusOptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. 'Paid").AppendLine()
                .Append("2. 'In Repair'").AppendLine()
                .Append("3. 'Repaired'").AppendLine()
                .Append("4. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowFuelTypes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Octan 95").AppendLine()
                .Append("2. Octan 96").AppendLine()
                .Append("3. Octan 98").AppendLine()
                .Append("4. Soler").AppendLine()
                .Append("5. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowPossibleVehicleTypes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Motorcycle")
                .AppendLine()
                .Append("2. Electrical Motorcycle").AppendLine()
                .Append("3. Car").AppendLine()
                .Append("4. Electrical Car").AppendLine()
                .Append("5. Truck").AppendLine()
                .Append("6. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowColorsOptionsForCars()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("================================================").AppendLine()
                .Append("1. Red").AppendLine()
                .Append("2. White").AppendLine()
                .Append("3. Black").AppendLine()
                .Append("4. Silver").AppendLine()
                .Append("5. Go back").AppendLine()
                .Append("================================================");
            Console.WriteLine(sb);
        }

        public static void ShowMotorcycleLicenseTypes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1. A").AppendLine()
                .Append("2. B1").AppendLine()
                .Append("3. AA").AppendLine()
                .Append("4. BB").AppendLine()
                .Append("5. Go back").AppendLine();
            Console.WriteLine(sb);
        }
    }
}