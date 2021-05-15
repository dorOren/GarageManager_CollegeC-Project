using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelBasedMotorcycle : FuelBasedVehicle
    {
        public string LicenseType { get; }
        public int EngieVolume { get; }
        List<Wheel> m_WheelArray;

        public FuelBasedMotorcycle(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,          //for Vehicle
            float i_RemainingFuelAmount,                                                                           //for FuelBasedVehicle
            string i_LicenseType, int i_EngieVolume,                                                               //for this
            string i_WheelManufacturer, int i_WheelCurrentAirPressure)                                             //for the wheels
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage,                                              //for Vehicle
                i_RemainingFuelAmount, 6, eFuelType.Octan98)                                           //for FuelBasedVehicle
        {
            LicenseType = i_LicenseType;
            EngieVolume = i_EngieVolume;
            for (int i = 0; i < 2; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 30));
            }
        }
    }
}