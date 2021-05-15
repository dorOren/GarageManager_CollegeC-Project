using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        public string Model { get; }
        public string RegistrationNumber { get; }
        public float EnergyPercentage { get; set; }

        public Vehicle(string i_Model, string i_RegistrationNumber, float i_EnergyPercentage)
        {
            Model = i_Model;
            RegistrationNumber = i_RegistrationNumber;
            EnergyPercentage = i_EnergyPercentage;
        }

    }
}