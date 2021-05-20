using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        public eColor Color { get; set; }
        public int NumberOfDoors { get; set; }
       
        public void SetFields(string i_Model, string i_RegistrationNumber,     //for Vehicle
            float i_RemainingBatteryTime,                                                                //for ElectricVehicle
            eColor i_Color, int i_NumberOfDoors,                                                         //for this
            string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                 //for the wheels
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_RemainingBatteryTime / 3.2f,                            //for Vehicle
                           i_RemainingBatteryTime, 3.2f);                                                //for ElectricVehicle
            Color = i_Color;
            NumberOfDoors = i_NumberOfDoors;
            for (int i = 0; i < 4; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 32));
            }
        }
    }
}