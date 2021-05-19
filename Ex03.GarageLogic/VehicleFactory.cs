using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {

        public Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Object newVehicle = new object();
            switch (i_VehicleType)
            {
                case eVehicleType.FuelBasedCar:
                    newVehicle= new FuelBasedCar();
                    break;
                    
                case eVehicleType.FuelBasedMotorcycle:
                    newVehicle= new FuelBasedMotorcycle();
                    break;

                case eVehicleType.Truck:
                    newVehicle = new Truck();
                    break;

                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;

                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle();
                    break;

            }

            return newVehicle as Vehicle;
        }
    }
}
/*
  public Vehicle CreateVehicle(
            eVehicleType i_VehicleType,                                                     // What to create
            string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,          // Vehicle class info
            string i_WheelManufacturer, float i_WheelCurrentAirPressure,                    // Vehicle's wheels info
            eFuelType i_FuelType, float i_RemainingFuelAmount,                              // FuelBased Vehicle info
            float i_RemainingBatteryTime,                                                   // Electric Vehicle info
            eColor i_Color, int i_NumberOfDoors,                                            // Car info
            eLicenseType i_LicenseType, int i_EngineVolume,                                 // Motorcycle info
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryWeight                     // Truck info
        )
        {
            Object newVehicle = new object();
            switch (i_VehicleType)
            {
                case eVehicleType.FuelBasedCar:
                    newVehicle= new FuelBasedCar(
                        i_Model, i_RegistrationNumber, i_EnergyPercentage,
                        i_RemainingFuelAmount,i_Color,i_NumberOfDoors,
                        i_WheelManufacturer,i_WheelCurrentAirPressure);
                    break;
                    
                case eVehicleType.FuelBasedMotorcycle:
                    newVehicle= new FuelBasedMotorcycle(
                        i_Model, i_RegistrationNumber, i_EnergyPercentage,
                        i_RemainingFuelAmount, i_LicenseType, i_EngineVolume,
                        i_WheelManufacturer, i_WheelCurrentAirPressure);
                    break;

                case eVehicleType.Truck:
                    newVehicle = new Truck(
                        i_Model, i_RegistrationNumber, i_EnergyPercentage,
                        i_RemainingFuelAmount, i_IsCarryingDangerousMaterials, i_MaxCarryWeight,
                        i_WheelManufacturer, i_WheelCurrentAirPressure);
                    break;

                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar(
                        i_Model, i_RegistrationNumber, i_EnergyPercentage,
                        i_RemainingBatteryTime, i_Color, i_NumberOfDoors,
                        i_WheelManufacturer, i_WheelCurrentAirPressure);
                    break;

                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle(
                        i_Model, i_RegistrationNumber, i_EnergyPercentage,
                        i_RemainingBatteryTime, i_LicenseType, i_EngineVolume,
                        i_WheelManufacturer, i_WheelCurrentAirPressure);
                    break;

            }

            return newVehicle as Vehicle;
        }
    }
*/