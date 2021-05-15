using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class InputHandler
    {
        public static int GetChosenOptionInMenuFromUser()
        {
            int res = 0;
            string input = Console.ReadLine();
            int chosenOpt;
            int.TryParse(input, out chosenOpt);
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
            float chosenOpt;
            float.TryParse(input, out chosenOpt);
            return chosenOpt;
        }


        public static int GetIntegerInputFromUser()
        {
            string input = Console.ReadLine();
            int chosenOpt;
            int.TryParse(input, out chosenOpt);
            return chosenOpt;
        }
    }
}