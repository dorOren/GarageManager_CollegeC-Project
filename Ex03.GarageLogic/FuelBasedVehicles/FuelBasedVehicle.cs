using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class FuelBasedVehicle : Vehicle
    {
        public eFuelType FuelType { get; set; }
        public float RemainingFuelAmount { get; set; }
        public float MaxFuelAmount { get; set; }

        
        public void SetFields(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
            float i_RemainingFuelAmount, float i_MaxFuelAmount, eFuelType i_FuelType)
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_EnergyPercentage);
            FuelType = i_FuelType;
            MaxFuelAmount = i_MaxFuelAmount;
            RemainingFuelAmount = i_RemainingFuelAmount;
        }
        
        public void FuelVehicle(float i_NumberOfLitersToAdd)
        {
            float predictedFuelAmount = i_NumberOfLitersToAdd + RemainingFuelAmount;
            if (predictedFuelAmount > MaxFuelAmount)
            {
                string error = $"Cannot add {i_NumberOfLitersToAdd} liters, fuel maximum value is {MaxFuelAmount}";
                throw new ValueOutOfRangeException(error, MaxFuelAmount);
            }
            else
            {
                RemainingFuelAmount += i_NumberOfLitersToAdd;
            }

        }

    }
}