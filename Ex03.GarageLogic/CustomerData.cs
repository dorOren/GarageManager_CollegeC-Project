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

    }
}
