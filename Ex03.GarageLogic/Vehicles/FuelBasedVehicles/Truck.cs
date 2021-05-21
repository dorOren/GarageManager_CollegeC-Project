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
        
        public void SetFields(string i_Model, string i_RegistrationNumber,                                                 //for Vehicle
                              float i_RemainingFuelAmount,                                                                 //for FuelBasedVehicle
                              bool i_IsCarryingDangerousMaterials, float i_MaxCarryWeight,                                 //for this
                              string i_WheelManufacturer, float i_WheelCurrentAirPressure)                                 //for the wheels
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_RemainingFuelAmount / 120f * 100,                //for Vehicle
                           i_RemainingFuelAmount, 120, eFuelType.Soler);                                       //for FuelBasedVehicle;
            if (i_MaxCarryWeight <= 0f)
            {
                throw new ArgumentException("Maximum carrying weight must be a positive number.");
            }
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
            return string.Format("{0}{1}Is carrying dangerous cargo: {1}{2}Max carry weight: {3}{2}",
                base.ToString(), IsCarryingDangerousMaterials, Environment.NewLine, MaxCarryWeight);
        }
    }
}