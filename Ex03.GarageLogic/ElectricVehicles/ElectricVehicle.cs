using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricalVehicle : Vehicle
    {
        public float RemainingBatteryTime { get; set; }
        public float MaxBatteryTime { get; }

        public ElectricalVehicle(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
            float i_RemainingBatteryTime, float i_MaxBatteryTime)
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage)
        {
            MaxBatteryTime = i_MaxBatteryTime;
            RemainingBatteryTime = i_RemainingBatteryTime;
        }


        public void ChargeBattery(float i_Hours)
        {
            float predictedBatteryTime = i_Hours + RemainingBatteryTime;
            if (predictedBatteryTime > MaxBatteryTime)
            {
                //throw ValueOutOfRangeException.
            }
            else
            {
                RemainingBatteryTime += i_Hours;
            }
        }
    }
}