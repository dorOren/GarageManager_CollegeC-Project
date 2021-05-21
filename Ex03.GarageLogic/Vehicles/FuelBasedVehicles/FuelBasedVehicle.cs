using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class FuelBasedVehicle : Vehicle
    {
        public eFuelType FuelType { get; set; }
        public float RemainingFuelAmount { get; set; }
        public float MaxFuelAmount { get; set; }

        
        public void SetFields(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
            float i_RemainingFuelAmount, float i_MaxFuelAmount, eFuelType i_FuelType)
        {
            base.SetFields(i_Model, i_RegistrationNumber, i_EnergyPercentage);
            if (i_RemainingFuelAmount > i_MaxFuelAmount)
            {
                string error = $"{i_RemainingFuelAmount} liters is more then fuel maximum value: {i_MaxFuelAmount}";
                throw new ValueOutOfRangeException(error,i_MaxFuelAmount);
            }
            if (i_RemainingFuelAmount <= 0f)
            {
                throw new ArgumentException("Remaining fuel amount must be a positive number.");
            }
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

        public override string ToString()
        { 
            return string.Format("{0}{2}Fuel type: {1}{2}Remaining fuel amount: {3}{2}Max fuel amount: {4}{2}",
                base.ToString(), FuelType.ToString(), Environment.NewLine, RemainingFuelAmount, MaxFuelAmount);
        }
    }
}