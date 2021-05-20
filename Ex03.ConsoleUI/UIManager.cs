using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Enums;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private GarageManager m_Garage;

        public UIManager()
        {
            m_Garage = new GarageManager();
        }

        public void Start()
        {
            bool runGarage = true;
            Console.Clear();
            while (runGarage)
            {

                Menu.ShowMainMenu();

                int chosenOpt = InputHandler.GetChosenOptionInMenuFromUser(8);
                switch (chosenOpt)
                {
                    case 1:
                        getNewVehicleDetails();
                        break;
                    case 2:
                        showLicenseNumbersInGarage(); // done
                        break;
                    case 3:
                        updateVehicleStatus(); // done
                        break;
                    case 4:
                        inflateWheels(); // done
                        break;
                    case 5:
                        fuelVehicle(); // done
                        break;
                    case 6:
                        chargeElectricVehicle(); // done
                        break;
                    case 7:
                        ShowFullVehicleData(); // done
                        break;
                    case 8:
                        runGarage = false;
                        break;
                    default:
                        PrintHandler.IllegalOptionOutput();
                        break;

                }
            }

        }


        private void getNewVehicleDetails()
        {// TODO: vehicle factory is currently not fit to this flow. Check with Dor.

            string licenseNumber = InputHandler.GetStringInputFromUser();

            if (isVehicleExistsInGarage(licenseNumber))
            {
                m_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.BeingRepaired);
                PrintHandler.VehicleIsAlreadyExistsInGarage();
            }
            else
            {
                eVehicleType vehicleType = getChosenVehicleTypeAsEnum();
                string modelName = InputHandler.GetStringInputFromUser();
                float currentWheelAirPressure = InputHandler.GetFloatInputFromUser();
                PrintHandler.AskForWheelManufacturer();
                string wheelManufacturer = InputHandler.GetStringInputFromUser();
                Vehicle vehicle = m_Garage.InitVehicle(vehicleType);
                if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle) || vehicleType.Equals(eVehicleType.ElectricMotorcycle))
                {
                    eLicenseType licenseType = getMotorCycleLicenseType();
                    int engineVolume = InputHandler.GetIntegerInputFromUser();
                    if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle))
                    {
                        PrintHandler.AskForCurrentFuelAmount();
                        float currentFuelAmount = InputHandler.GetFloatInputFromUser();
                        (vehicle as FuelBasedMotorcycle).SetFields(modelName, licenseNumber, currentFuelAmount, licenseType, engineVolume, wheelManufacturer, currentWheelAirPressure);
                    }
                    else
                    {
                        PrintHandler.AskForCurrentBatteryTime();
                        float currentBatteryTime = InputHandler.GetFloatInputFromUser();
                        (vehicle as ElectricMotorcycle).SetFields(modelName, licenseNumber, currentBatteryTime, licenseType, engineVolume, wheelManufacturer, currentWheelAirPressure);

                    }

                }
                else if (vehicleType.Equals(eVehicleType.FuelBasedCar) || vehicleType.Equals(eVehicleType.ElectricCar))
                {

                    eColor chosenColor = getColorFromUser();
                    int numDoors = getNumberOfDoorsFromUSer();
                    if (vehicleType.Equals(eVehicleType.FuelBasedCar))
                    {
                        PrintHandler.AskForCurrentFuelAmount();
                        float currentFuelAmount = InputHandler.GetFloatInputFromUser();
                        (vehicle as FuelBasedCar).SetFields(modelName, licenseNumber, currentFuelAmount, chosenColor, numDoors, wheelManufacturer, currentWheelAirPressure);
                    }
                    else
                    {
                        PrintHandler.AskForCurrentBatteryTime();
                        float currentBatteryTime = InputHandler.GetFloatInputFromUser();
                        (vehicle as ElectricCar).SetFields(modelName, licenseNumber, currentBatteryTime, chosenColor, numDoors, wheelManufacturer, currentWheelAirPressure);
                    }

                }
                else if (vehicleType.Equals(eVehicleType.Truck))
                {

                    bool isCarryingDangerousCargo = isTruckCarryingDangerousCargo();
                    float maxCarryingWeight = InputHandler.GetFloatInputFromUser();
                    PrintHandler.AskForCurrentFuelAmount();
                    float currentFuelAmount = InputHandler.GetFloatInputFromUser();
                    (vehicle as Truck).SetFields(modelName, licenseNumber, currentFuelAmount, isCarryingDangerousCargo, maxCarryingWeight, wheelManufacturer, currentWheelAirPressure);
                }
                // TODO: get owner details and send to GarageManager to insert to phonebook. AddCustomerDetailsToBook
                getCustomerDetails(licenseNumber);
            }

        }

        private bool isVehicleExistsInGarage(string i_LicenseNumber)
        {
            return m_Garage.FindVehicleInGarage(i_LicenseNumber) != null;
        }

        private eLicenseType getMotorCycleLicenseType()
        {
            PrintHandler.AskForMotorcycleLicenseType();
            Menu.ShowMotorcycleLicenseTypes();
            int chosenType = InputHandler.GetChosenOptionInMenuFromUser(4);
            eLicenseType resLicenseType = eLicenseType.None;
            switch(chosenType)
            {
                case 1:
                    resLicenseType = eLicenseType.A;
                    break;
                case 2:
                    resLicenseType = eLicenseType.B1;
                    break;
                case 3:
                    resLicenseType = eLicenseType.AA;
                    break;
                case 4:
                    resLicenseType = eLicenseType.BB;
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }
            return resLicenseType;
        }

        private int getNumberOfDoorsFromUSer()
        {
            int res = 0;
            bool valid = false;
            while (!valid)
            {
                res = InputHandler.GetIntegerInputFromUser();
                valid = res >= 2 && res <= 5;
            }

            return res;
        }

        private eColor getColorFromUser()
        { //TODO: Default value for enum?
            PrintHandler.AskForColor();
            Menu.ShowColorsOptionsForCars();
            int chosenOpt = InputHandler.GetIntegerInputFromUser();
            eColor resColor = 0;
            switch (chosenOpt)
            {
                case 1:
                    resColor = eColor.Red;
                    break;
                case 2:
                    resColor = eColor.Black;
                    break;
                case 3:
                    resColor = eColor.Silver;
                    break;
                case 4:
                    resColor = eColor.White;
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }

            return resColor;
        }

        private eVehicleType getChosenVehicleTypeAsEnum()
        { // TODO: Add default value to enum class eVehicleType
            eVehicleType chosenTypeResult = eVehicleType.None;
            PrintHandler.AskToChooseVehicleType();
            Menu.ShowPossibleVehicleTypes();
            int chosenVehicleOpt = InputHandler.GetChosenOptionInMenuFromUser(5);
            switch (chosenVehicleOpt)
            {
                case 1:
                    chosenTypeResult = eVehicleType.FuelBasedMotorcycle;
                    break;
                case 2:
                    chosenTypeResult = eVehicleType.ElectricMotorcycle;
                    break;
                case 3:
                    chosenTypeResult = eVehicleType.FuelBasedCar;
                    break;
                case 4:
                    chosenTypeResult = eVehicleType.ElectricCar;
                    break;
                case 5:
                    chosenTypeResult = eVehicleType.Truck;
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }

            return chosenTypeResult;
        }

        private void ShowFullVehicleData()
        {// TODO: 1. Maybe should add an exception for returning empty list.
            string i_LicenseNumber = InputHandler.GetStringInputFromUser();
            StringBuilder vehicleDetails = m_Garage.ShowVehicleData(i_LicenseNumber);
            PrintHandler.PrintVehicleDetails(vehicleDetails);
        }

        private void chargeElectricVehicle()
        {//TODO: 1. GarageLogic.UpdateVehicleStatus; 2. Insert exceptions; 
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            int amountOfBatteryTimeToAdd = InputHandler.GetIntegerInputFromUser();
            m_Garage.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
        }

        private void fuelVehicle()
        { // TODO: 1. GarageLogic.FuelVehicle; 2. Exceptions; 
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            PrintHandler.AskToChooseFuelType();
            Menu.ShowFuelTypes();
            eFuelType chosenFuelType = getFuelTypeAndConvertToEnum();
            PrintHandler.AskForFuelingAmount();
            float amountToFuel = InputHandler.GetFloatInputFromUser();
            m_Garage.FuelVehicle(inputLicenseNumber, chosenFuelType, amountToFuel);
        }

        private eFuelType getFuelTypeAndConvertToEnum()
        {
            int chosenFuelType = InputHandler.GetChosenOptionInMenuFromUser(4);
            eFuelType typeResult = eFuelType.None;
            switch (chosenFuelType)
            {
                case 1:
                    typeResult = eFuelType.Soler;
                    break;
                case 2:
                    typeResult = eFuelType.Octan95;
                    break;
                case 3:
                    typeResult = eFuelType.Octan96;
                    break;
                case 4:
                    typeResult = eFuelType.Octan98;
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }

            return typeResult;
        }


        private void inflateWheels()
        {// TODO: GarageLogic.InflateToMax
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            m_Garage.InflateToMax(inputLicenseNumber);
        }

        private void updateVehicleStatus()
        {//TODO: 1. GarageLogic.UpdateVehicleStatus; 2. Insert exceptions in switch statement; 
            Console.WriteLine("Enter license number of the vehicle you wish to update:");
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();

            Console.WriteLine("Choose the vehicle's new status: ");
            Menu.ShowUpdatingOptionsByVehicleStatusMenu();
            int statusChosen = InputHandler.GetChosenOptionInMenuFromUser(4);
            switch (statusChosen)
            {
                case 1:
                    m_Garage.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.Paid);
                    break;
                case 2:
                    m_Garage.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.BeingRepaired);
                    break;
                case 3:
                    m_Garage.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.Repaired);
                    break;
                case 4:
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }


        }

        private void showLicenseNumbersInGarage()
        {
            bool runMenu = true;
            Console.Clear();
            while (runMenu)
            {
                int chosenOpt = -1;
                Menu.ShowFilteringOptionsByVehicleStatusMenu();
                try
                {
                    chosenOpt = InputHandler.GetChosenOptionInMenuFromUser(5);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex);
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex);
                }

                if (chosenOpt == -1) continue;
                List<string> resLicenses = new List<string>();
                switch (chosenOpt)
                {
                    case 1:
                        resLicenses = m_Garage.ShowAllVehiclesUnderCare(eVehicleStatus.Paid);
                        break;
                    case 2:
                        resLicenses = m_Garage.ShowAllVehiclesUnderCare(eVehicleStatus.BeingRepaired);
                        break;
                    case 3:
                        resLicenses = m_Garage.ShowAllVehiclesUnderCare(eVehicleStatus.Repaired);
                        break;
                    case 4:
                        resLicenses = m_Garage.ShowAllVehiclesUnderCare(eVehicleStatus.Any);
                        break;
                    case 5:
                        runMenu = false;
                        break;
                    default:
                        PrintHandler.IllegalOptionOutput();
                        break;
                }

                if (resLicenses.Count > 0)
                {
                    Console.Clear();
                    Menu.ShowFilteringOptionsByVehicleStatusMenu();
                    PrintHandler.PrintList(resLicenses);
                }

            }

        }
    }
}