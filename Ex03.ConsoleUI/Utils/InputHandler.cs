using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class InputHandler
    {
        public static int GetChosenOptionInMenuFromUser(int i_NumberOfOptions)
        {
            string input = Console.ReadLine();
            int chosenOpt = int.Parse(input);
            if (i_NumberOfOptions < chosenOpt || chosenOpt<1)
            {
                throw new ArgumentException("You chose an illegal option from the menu.");
            }
            return chosenOpt;
        }

        public static string GetStringInputFromUser()
        {
            string input = Console.ReadLine();
            return input;
        }

        public static float GetFloatInputFromUser()
        {
            string input = Console.ReadLine();
            float chosenOpt= float.Parse(input);
            return chosenOpt;
        }


        public static int GetIntegerInputFromUser()
        {
            string input = Console.ReadLine();
            int chosenOpt= int.Parse(input);
            return chosenOpt;
        }

        public static bool GetBooleanInputFromUser()
        {
            string input = Console.ReadLine();
            bool res = char.Parse(input) == 'y';
            return res;
        }
    }
}