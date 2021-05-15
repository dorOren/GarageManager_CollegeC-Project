using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class VehicleFactory
    {
        enum eVehicleType
        {
            FuelBasedCar,
            FuelBasedMotorcycle,
            Truck,
            ElecticCar,
            ElectricMotorcycle
        }

        public Vehicle CreateVehicle(string i_VehicleType,
            string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
            float RemainingEnergySource)
        {
            if (nameof(eVehicleType.FuelBasedCar).Equals(i_VehicleType))
            {
               // return new FuelBasedCar();
            }
            else if (nameof(eVehicleType.FuelBasedMotorcycle).Equals(i_VehicleType))
            {

            }
            else if (nameof(eVehicleType.Truck).Equals(i_VehicleType))
            {

            }
            else if (nameof(eVehicleType.ElecticCar).Equals(i_VehicleType))
            {

            }
            else if (nameof(eVehicleType.ElectricMotorcycle).Equals(i_VehicleType))
            {

            }


        }
    }
}