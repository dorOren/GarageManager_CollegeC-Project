using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class CustomersDetailsBook
    {
        private readonly Dictionary<string, CustomerData> m_CustomerBook;

        public CustomersDetailsBook()
        {
            m_CustomerBook = new Dictionary<string, CustomerData>();
        }

        public void AddCustomer(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhone)
        {
            CustomerData data = new CustomerData(i_OwnerName, i_OwnerPhone);
            m_CustomerBook.Add(i_LicenseNumber, data);
        }

        public void RemoveCustomer(string i_LicenseNumber)
        {
            m_CustomerBook.Remove(i_LicenseNumber);
        }

        public CustomerData GetCustomerData(string i_LicenseNumber)
        {
            return m_CustomerBook[i_LicenseNumber];
        }

        public void UpdateVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
        {
            string ownerName = m_CustomerBook[i_LicenseNumber].OwnerName;
            string ownerPhone = m_CustomerBook[i_LicenseNumber].OwnerPhone;
            eVehicleStatus vehicleStatus = i_NewStatus;

            m_CustomerBook[i_LicenseNumber] = new CustomerData(ownerName, ownerPhone, vehicleStatus);

        }

        public bool FindKey(string i_KeyToFind)
        {
            if (m_CustomerBook.ContainsKey(i_KeyToFind))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}