using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class FuelBasedMotorcycle : FuelBasedVehicle
    {
        public eLicenseType LicenseType { get; set; }
        public int EngineVolume { get; set; }
        
        public void SetFields(string i_Model, string i_RegistrationNumber,                           //for Vehicle
            float i_RemainingFuelAmount,                                                                                      //for FuelBasedVehicle
            eLicenseType i_LicenseType, int i_EngineVolume,                                                                   //for this
            string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                                      //for the wheels
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_RemainingFuelAmount / 6f,                                                 //for Vehicle
                i_RemainingFuelAmount, 6, eFuelType.Octan98);                                                     //for FuelBasedVehicle);
            LicenseType = i_LicenseType;
            EngineVolume = i_EngineVolume;
            for (int i = 0; i < 2; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 30));
            }
        }
        
    }
}