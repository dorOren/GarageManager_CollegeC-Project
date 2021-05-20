using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public struct  CustomerData
    {

        public CustomerData(string i_OwnerName, string i_OwnerPhone)
        {
            OwnerName = i_OwnerName;
            OwnerPhone = i_OwnerPhone;
            VehicleStatus = eVehicleStatus.BeingRepaired;
        }

        public CustomerData(string i_OwnerName, string i_OwnerPhone, eVehicleStatus i_Status)
        {
            OwnerName = i_OwnerName;
            OwnerPhone = i_OwnerPhone;
            VehicleStatus = i_Status;
        }
        public string OwnerName
        {
            get;
            set;
        }
        public string OwnerPhone
        {
            get;
            set;
        }
        public eVehicleStatus VehicleStatus
        {
            get;
            set;
        }
        public CustomerData(string i_OwnerName, string i_OwnerPhone)
        {
            OwnerName = i_OwnerName;
            OwnerPhone = i_OwnerPhone;
            VehicleStatus = eVehicleStatus.BeingRepaired;
        }
    }
}
