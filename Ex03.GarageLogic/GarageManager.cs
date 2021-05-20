using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;


namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private CustomersDetailsBook m_Book;
        private VehicleFactory m_Factory;
        private List<Vehicle> m_VehiclesInGarage;

        public GarageManager()
        {
            m_Book = new CustomersDetailsBook();
            m_Factory = new VehicleFactory();
            m_VehiclesInGarage = new List<Vehicle>();
        }

        
        public Vehicle InitVehicle(eVehicleType i_VehicleType)
        {
            return m_Factory.CreateVehicle(i_VehicleType);
        }

        public void AddCustomerDetailsToBook(string i_OwnerName, string i_OwnerPhoneNumber, string i_RegistrationNumber)
        {
            if (!m_Book.FindKey(i_RegistrationNumber))
            {
                m_Book.AddCustomer(i_RegistrationNumber, i_OwnerName, i_OwnerPhoneNumber);
            }
        }
        

        public Vehicle FindVehicleInGarage(string i_RegistrationNumber)
        {
            Object requestedVehicle = new object();
            bool found = false;
            foreach (Vehicle vehicle in m_VehiclesInGarage)
            {
                if (i_RegistrationNumber == vehicle.RegistrationNumber)
                {
                    requestedVehicle = vehicle;
                    found = true;
                }
            }

            if (!found)
            {
                throw new ArgumentException("Requested vehicle is not in the garage.");
            }

            return requestedVehicle as Vehicle;
        }

        public void UpdateVehicleStatus(string i_RegistrationNumber, eVehicleStatus i_VehicleStatus)
        {
            if(!(m_Book.FindKey(i_RegistrationNumber)))
            {
                throw new ArgumentException("Requested vehicle is not in the garage.");
            }
            else
            {
                m_Book.UpdateVehicleStatus(i_RegistrationNumber, i_VehicleStatus);
            }
            
        }

        public List<string> ShowAllVehiclesUnderCare(eVehicleStatus i_VehicleStatus)
        {
            List<string> data = new List<string>();
            data.Add("List of all vehicle's registration numbers in garage:");
            foreach (Vehicle vehicle in m_VehiclesInGarage)
            {
                CustomerData customerData = m_Book.GetCustomerData(vehicle.RegistrationNumber);
                if (customerData.VehicleStatus.Equals(i_VehicleStatus) || i_VehicleStatus.Equals(eVehicleStatus.Any))
                {
                    data.Add(vehicle.RegistrationNumber);
                }
            }

            if (data.Count == 1)
            {
                data.Add("None");
            }

            return data;
        }

        public void InflateToMax(string i_RegistrationNumber)
        {
            Vehicle vehicle = FindVehicleInGarage(i_RegistrationNumber);
            foreach (Wheel wheel in vehicle.m_WheelArray)
            {
                wheel.InflateToMax();
            }
        }

        public void FuelVehicle(string i_RegistrationNumber, eFuelType i_FuelType, float i_FuelAmount)
        {
            Vehicle vehicle = FindVehicleInGarage(i_RegistrationNumber);
            if (vehicle is FuelBasedVehicle)
            {
                if (vehicle is FuelBasedCar)
                {
                    if ((vehicle as FuelBasedCar).FuelType == i_FuelType)
                    {
                        (vehicle as FuelBasedVehicle).FuelVehicle(i_FuelAmount);
                    }
                    else
                    {
                        throw new ArgumentException("Requested fuel Type does not match vehicle's fuel type.");
                    }

                }
                else if (vehicle is FuelBasedMotorcycle)
                {
                    if ((vehicle as FuelBasedMotorcycle).FuelType == i_FuelType)
                    {
                        (vehicle as FuelBasedVehicle).FuelVehicle(i_FuelAmount);
                    }
                    else
                    {
                        throw new ArgumentException("Requested fuel Type does not match vehicle's fuel type.");
                    }
                }
                else if (vehicle is Truck)
                {
                    if ((vehicle as Truck).FuelType == i_FuelType)
                    {
                        (vehicle as FuelBasedVehicle).FuelVehicle(i_FuelAmount);
                    }
                    else
                    {
                        throw new ArgumentException("Requested fuel Type does not match vehicle's fuel type.");
                    }
                }

            }
            else
            {
                throw new ArgumentException("This vehicle is not fuel based.");
            }
        }

        public void ChargeBattery(string i_RegistrationNumber, float i_Minutes)
        {
            Vehicle vehicle = FindVehicleInGarage(i_RegistrationNumber);
           
            if (vehicle is ElectricVehicle)
            {
                (vehicle as ElectricVehicle).ChargeBattery(i_Minutes / 60);
            }
            else
            {
                throw new ArgumentException("This vehicle is not electric.");
            }
        }

        
        public StringBuilder ShowVehicleData(string i_RegistrationNumber)
        {
            Vehicle requestedVehicle = null;
            StringBuilder data = new StringBuilder();
            try
            {
                requestedVehicle = FindVehicleInGarage(i_RegistrationNumber);
            }
            catch
            {
                data.AppendLine("There are no vehicles in the garage.");
                return data;
            }

            data.Append(AddBookData(i_RegistrationNumber));
            data.Append(AddVehicleData(requestedVehicle));
            data.Append(AddWheelsData(requestedVehicle));
            data.Append($"Vehicle info:{Environment.NewLine}");
            switch (requestedVehicle)
            {
                case ElectricVehicle vehicle:
                    data.Append(AddElectricVehicleData(vehicle));
                    break;
                case FuelBasedVehicle vehicle:
                    data.Append(AddFuelBasedVehicleData(vehicle));
                    break;
            }
            return data;
        }

        public StringBuilder AddBookData(string i_RegistrationNumber)
        {
            StringBuilder data = new StringBuilder();
            CustomerData customerDetails = m_Book.GetCustomerData(i_RegistrationNumber);
            data.Append($"Customer details:{Environment.NewLine}");
            data.Append($"Owner's name: {customerDetails.OwnerName}{Environment.NewLine}");
            data.Append($"Owner's phone number: {customerDetails.OwnerPhone}{Environment.NewLine}");
            data.Append($"Owner's vehicle status: {customerDetails.VehicleStatus}{Environment.NewLine}");
            data.Append(Environment.NewLine);
            return data;
        }

        public StringBuilder AddVehicleData(Vehicle i_RequestedVehicle)
        {
            StringBuilder data = new StringBuilder();
            data.Append($"Identifying information:{Environment.NewLine}");
            data.Append($"Registration Number: {i_RequestedVehicle.RegistrationNumber}{Environment.NewLine}");
            data.Append($"Vehicle's model: {i_RequestedVehicle.Model}{Environment.NewLine}");
            data.Append(Environment.NewLine);
            return data;
        }

        public StringBuilder AddWheelsData(Vehicle i_RequestedVehicle)
        {
            StringBuilder data = new StringBuilder();
            data.Append($"Wheel's info:{Environment.NewLine}");
            data.Append($"Wheels manufacturer: {i_RequestedVehicle.m_WheelArray[0].Manufacturer}{Environment.NewLine}");
            data.Append($"Wheels maximum air pressure: {i_RequestedVehicle.m_WheelArray[0].MaxAirPressure}{Environment.NewLine}");
            data.Append($"Wheels current air pressure: {i_RequestedVehicle.m_WheelArray[0].CurrentAirPressure}{Environment.NewLine}");
            data.Append(Environment.NewLine);
            return data;
        }

        public StringBuilder AddElectricVehicleData(ElectricVehicle i_RequestedVehicle)
        {
            StringBuilder data = new StringBuilder();

            data.Append($"Max battery time (in hours): {i_RequestedVehicle.MaxBatteryTime}{Environment.NewLine}");
            data.Append($"Remaining battery time (in hours): {i_RequestedVehicle.RemainingBatteryTime}{Environment.NewLine}");

            switch (i_RequestedVehicle)
            {
                case ElectricCar car:
                    data.Append($"Color: {car.Color}{Environment.NewLine}");
                    data.Append($"Number of doors: {car.NumberOfDoors}{Environment.NewLine}");
                    break;
                case ElectricMotorcycle motorcycle:
                    data.Append($"License Type: {motorcycle.LicenseType}{Environment.NewLine}");
                    data.Append($"Engine Volume: {motorcycle.EngineVolume}{Environment.NewLine}");
                    break;
            }

            return data;
        }

        public StringBuilder AddFuelBasedVehicleData(FuelBasedVehicle i_RequestedVehicle)
        {
            StringBuilder data = new StringBuilder();
            data.Append($"Fuel type: {i_RequestedVehicle.FuelType}{Environment.NewLine}");
            data.Append($"Max Fuel amount: {i_RequestedVehicle.MaxFuelAmount}{Environment.NewLine}");
            data.Append($"Current Fuel amount: {i_RequestedVehicle.RemainingFuelAmount}{Environment.NewLine}");

            switch (i_RequestedVehicle)
            {
                case FuelBasedCar car:
                    data.Append($"Color: {car.Color}{Environment.NewLine}");
                    data.Append($"Number of doors: {car.NumberOfDoors}{Environment.NewLine}");
                    break;
                case FuelBasedMotorcycle motorcycle:
                    data.Append($"License Type: {motorcycle.LicenseType}{Environment.NewLine}");
                    data.Append($"Engine Volume: {motorcycle.EngineVolume}{Environment.NewLine}");
                    break;
                case Truck truck:
                {
                    string answer = truck.IsCarryingDangerousMaterials ? "Yes" : "No";
                    data.Append($"Is carrying dangerous materials: {answer}{Environment.NewLine}");
                    data.Append($"Maximum carry weight: {truck.MaxCarryWeight}{Environment.NewLine}");
                    break;
                }
            }

            return data;
        }

 public void AddToVehiclesListDB(Vehicle i_Vehicle)
        {
            m_VehiclesInGarage.Add(i_Vehicle);
        }
    }
}
