using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelBasedCar : FuelBasedVehicle
    {
        public eColor Color { get; set; }
        public int NumberOfDoors { get; }
        List<Wheel> m_WheelArray;

        public FuelBasedCar(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,                    //for Vehicle
            float i_RemainingFuelAmount,                                                                              //for FuelBasedVehicle
            eColor i_Color, int i_NumberOfDoors,                                                                      //for this
            string i_WheelManufacturer, int i_WheelCurrentAirPressure)                                                //for the wheels
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage,                                                 //for Vehicle
                i_RemainingFuelAmount, 45, eFuelType.Octan95)                                             //for FuelBasedVehicle
        {
            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
            for (int i = 0; i < 4; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 32));
            }
        }
    }
}