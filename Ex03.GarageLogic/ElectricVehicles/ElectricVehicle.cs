using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class ElectricVehicle : Vehicle
    {
        public float RemainingBatteryTime { get; set; }
        public float MaxBatteryTime { get; set; }
     
        public void SetFields(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
                         float i_RemainingBatteryTime, float i_MaxBatteryTime)
        {
            if (i_RemainingBatteryTime > i_MaxBatteryTime)
            {
                string error = $"{i_RemainingBatteryTime} hours of battery time is more then maximum value of battery time: {i_MaxBatteryTime}";
                throw new ValueOutOfRangeException(error, i_MaxBatteryTime);
            }
            if (i_RemainingBatteryTime <= 0f)
            {
                throw new ArgumentException("Remaining battery time must be a positive number.");
            }
            base.SetFields(i_Model, i_RegistrationNumber, i_EnergyPercentage);
            MaxBatteryTime = i_MaxBatteryTime;
            RemainingBatteryTime = i_RemainingBatteryTime;
        }
      

        public void ChargeBattery(float i_Hours)
        {
            float predictedBatteryTime = i_Hours + RemainingBatteryTime;
            if (predictedBatteryTime > MaxBatteryTime)
            {
                string error = $"Cannot add {i_Hours} hours of battery time, maximum hours of battery time is {MaxBatteryTime}";
                throw new ValueOutOfRangeException(error, MaxBatteryTime);
            }
            else
            {
                RemainingBatteryTime += i_Hours;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "Remaining Battery time: " + RemainingBatteryTime + Environment.NewLine + "Max battery time: " + MaxBatteryTime + Environment.NewLine;
        }
    }
}