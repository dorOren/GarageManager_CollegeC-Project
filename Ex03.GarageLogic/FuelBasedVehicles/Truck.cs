using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class Truck : FuelBasedVehicle
    {
        public bool isCarryingDangerousMaterials { get; set; }
        public float MaxCarryWeight { get; }
        List<Wheel> m_WheelArray;

        public Truck(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,                    //for Vehicle
            float i_RemainingFuelAmount,                                                                       //for FuelBasedVehicle
            bool i_isCarryingDangerousMaterials, float i_MaxCarryWeight,                                       //for this
            string i_WheelManufacturer, int i_WheelCurrentAirPressure)                                         //for the wheels
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage,                                          //for Vehicle
                i_RemainingFuelAmount, 120, eFuelType.Soler)                                       //for FuelBasedVehicle
        {
            isCarryingDangerousMaterials = i_isCarryingDangerousMaterials;
            MaxCarryWeight = i_MaxCarryWeight;
            for (int i = 0; i < 16; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 26));
            }
        }
    }
}