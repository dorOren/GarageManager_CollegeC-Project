using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelBasedCar : FuelBasedVehicle
    {
        public eColor Color { get; set; }
        public int NumberOfDoors { get; set; }

        public void SetFields(string i_Model, string i_RegistrationNumber,                                                       //for Vehicle
                              float i_RemainingFuelAmount,                                                                       //for FuelBasedVehicle
                              eColor i_Color, int i_NumberOfDoors,                                                               //for this
                              string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                       //for the wheels
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_RemainingFuelAmount / 45f * 100,                       //for Vehicle
                           i_RemainingFuelAmount, 45, eFuelType.Octan95);                                            //for FuelBasedVehicle;
            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
            m_WheelArray = new List<Wheel>(4);
            for (int i = 0; i < 4; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 32));
            }
        }

        public override string ToString()
        {
            return base.ToString() + "Color: " + Color + Environment.NewLine + "Number of doors: " + NumberOfDoors + Environment.NewLine;
        }
    }
}