using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public string Manufacturer { get; }
        public float CurrentAirPressure { get; set; }
        public float MaxAirPressure { get; }

        
        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                string error = $"{i_CurrentAirPressure} air pressure is more then air pressure maximum value: {i_MaxAirPressure}";
                throw new ValueOutOfRangeException(error, i_MaxAirPressure);
            }
            if (i_CurrentAirPressure < 1)
            {
                throw new ArgumentException("Current air pressure must be a positive number.");
            }
            Manufacturer = i_Manufacturer;
            CurrentAirPressure = i_CurrentAirPressure;
            MaxAirPressure = i_MaxAirPressure;
        }
        

        public void InflateWheel(float i_HowMuchToAdd)
        {
            float predictedAirPressure = i_HowMuchToAdd + CurrentAirPressure;
            if (predictedAirPressure > MaxAirPressure)
            {
                string error = $"Cannot add {i_HowMuchToAdd} air pressure, air pressure's maximum value is {MaxAirPressure}";
                throw new ValueOutOfRangeException(error, MaxAirPressure);
            }
            else
            {
                CurrentAirPressure += i_HowMuchToAdd;
            }
        }

        public void InflateToMax()
        {
            CurrentAirPressure = MaxAirPressure;
        }
    }
}