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
                    newVehicle = new FuelBasedCar();
                    break;

                case eVehicleType.FuelBasedMotorcycle:
                    newVehicle = new FuelBasedMotorcycle();
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