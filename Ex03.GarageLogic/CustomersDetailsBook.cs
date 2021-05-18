using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CustomersDetailsBook
    {
        private readonly Dictionary<string, CustomerData> m_CustomerBook;

        public CustomersDetailsBook()
        {
            m_CustomerBook = new Dictionary<string, CustomerData>();
        }

        public void AddCustomer(string i_LicenseNumber, CustomerData i_Data)
        {
            m_CustomerBook.Add(i_LicenseNumber, i_Data);
        }

        public void RemoveCustomer(string i_LicenseNumber)
        {
            m_CustomerBook.Remove(i_LicenseNumber);
        }

        public CustomerData GetCustomerData(string i_LicenseNumber)
        {
            return m_CustomerBook[i_LicenseNumber];
        }
    }
}