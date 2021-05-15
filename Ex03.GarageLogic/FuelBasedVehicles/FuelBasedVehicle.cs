using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class FuelBasedVehicle : Vehicle
    {
        public eFuelType FuelType { get; set; }
        public float RemainingFuelAmount { get; set; }
        public float MaxFuelAmount { get; set; }


        public FuelBasedVehicle(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage,
            float i_RemainingFuelAmount, float i_MaxFuelAmount, eFuelType i_FuelType)
            : base(i_Model, i_RegistrationNumber, i_EnergyPercentage)
        {
            FuelType = i_FuelType;
            MaxFuelAmount = i_MaxFuelAmount;
            RemainingFuelAmount = i_RemainingFuelAmount;
        }

        public void FeulVehicle(float i_NumberOfLitersToAdd)
        {
            float predictedFuelAmount = i_NumberOfLitersToAdd + RemainingFuelAmount;
            if (predictedFuelAmount > MaxFuelAmount)
            {
                //throw ValueOutOfRangeException.
            }
            else
            {
                RemainingFuelAmount += i_NumberOfLitersToAdd;
            }

        }

    }
}