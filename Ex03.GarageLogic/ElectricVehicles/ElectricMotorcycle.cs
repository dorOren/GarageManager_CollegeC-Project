using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public eLicenseType LicenseType { get; }
        public int EngineVolume { get; }
        
        public ElectricMotorcycle(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,            //for Vehicle
            float i_RemainingBatteryTime,                                                                           //for ElectricVehicle
            eLicenseType i_LicenseType, int i_EngineVolume,                                                         //for this
            string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                            //for the wheels
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage,                                               //for Vehicle
                i_RemainingBatteryTime, 1.8f)                                                          //for ElectricVehicle
        {   
            LicenseType = i_LicenseType;
            EngineVolume = i_EngineVolume;
            for (int i = 0; i < 2; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 30));
            }
        }
    }
}