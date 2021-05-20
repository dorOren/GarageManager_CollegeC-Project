using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : FuelBasedVehicle
    {
        public bool IsCarryingDangerousMaterials { get; set; }
        public float MaxCarryWeight { get; set; }
        
        public void SetFields(string i_Model, string i_RegistrationNumber,                                     //for Vehicle
            float i_RemainingFuelAmount,                                                                       //for FuelBasedVehicle
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryWeight,                                       //for this
            string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                       //for the wheels
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_RemainingFuelAmount / 120f,          //for Vehicle
                i_RemainingFuelAmount, 120, eFuelType.Soler);                                      //for FuelBasedVehicle;
            IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
            MaxCarryWeight = i_MaxCarryWeight;
            m_WheelArray = new List<Wheel>(16);
            for (int i = 0; i < 16; i++)
            {
                m_WheelArray.Add(new Wheel(i_WheelManufacturer, i_WheelCurrentAirPressure, 26));
            }
        }

        public override string ToString()
        {
            
            return base.ToString() + Environment.NewLine + "Is carrying dangerous cargo: " + IsCarryingDangerousMaterials + Environment.NewLine
                   + "Max carry weight: " + MaxCarryWeight + Environment.NewLine;
        }
    }
}