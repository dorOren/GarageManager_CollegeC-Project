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
        {

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
                if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle) ||
                    vehicleType.Equals(eVehicleType.ElectricMotorcycle))
                {
                    eLicenseType licenseType = getMotorCycleLicenseType();
                    int engineVolume = InputHandler.GetIntegerInputFromUser();
                    if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle))
                    {
                        PrintHandler.AskForCurrentFuelAmount();
                        float currentFuelAmount = InputHandler.GetFloatInputFromUser();
                        (vehicle as FuelBasedMotorcycle).SetFields(modelName, licenseNumber, currentFuelAmount,
                            licenseType, engineVolume, wheelManufacturer, currentWheelAirPressure);
                    }
                    else
                    {
                        PrintHandler.AskForCurrentBatteryTime();
                        float currentBatteryTime = InputHandler.GetFloatInputFromUser();
                        (vehicle as ElectricMotorcycle).SetFields(modelName, licenseNumber, currentBatteryTime,
                            licenseType, engineVolume, wheelManufacturer, currentWheelAirPressure);

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
                        (vehicle as FuelBasedCar).SetFields(modelName, licenseNumber, currentFuelAmount, chosenColor,
                            numDoors, wheelManufacturer, currentWheelAirPressure);
                    }
                    else
                    {
                        PrintHandler.AskForCurrentBatteryTime();
                        float currentBatteryTime = InputHandler.GetFloatInputFromUser();
                        (vehicle as ElectricCar).SetFields(modelName, licenseNumber, currentBatteryTime, chosenColor,
                            numDoors, wheelManufacturer, currentWheelAirPressure);
                    }

                }
                else if (vehicleType.Equals(eVehicleType.Truck))
                {

                    bool isCarryingDangerousCargo = isTruckCarryingDangerousCargo();
                    float maxCarryingWeight = InputHandler.GetFloatInputFromUser();
                    PrintHandler.AskForCurrentFuelAmount();
                    float currentFuelAmount = InputHandler.GetFloatInputFromUser();
                    (vehicle as Truck).SetFields(modelName, licenseNumber, currentFuelAmount, isCarryingDangerousCargo,
                        maxCarryingWeight, wheelManufacturer, currentWheelAirPressure);
                }

                // TODO: get owner details and send to GarageManager to insert to phonebook. AddCustomerDetailsToBook
                getCustomerDetails(licenseNumber);
            }

        }

        private void getCustomerDetails(string i_LicenseNumber)
        {
            PrintHandler.AskForOwnerName();
            string ownerName = InputHandler.GetStringInputFromUser();
            PrintHandler.AskForOwnerPhone();
            string ownerPhone = InputHandler.GetStringInputFromUser();
            m_Garage.AddCustomerDetailsToBook(ownerName, ownerPhone, i_LicenseNumber);
        }

        private bool isTruckCarryingDangerousCargo()
        {
            PrintHandler.AskIfTruckIsCarryingDangerousCargo();
            string chosenOpt = InputHandler.GetStringInputFromUser();
            bool res = char.Parse(chosenOpt) == 'y';
            return res;
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
            switch (chosenType)
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
        {
            //TODO: Default value for enum?
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
        {
            bool closeMenu = false;
            eVehicleType chosenTypeResult = eVehicleType.None;

            while (!closeMenu)
            {
                try
                {
                    PrintHandler.AskToChooseVehicleType();
                    Menu.ShowPossibleVehicleTypes();
                    int chosenVehicleOpt = InputHandler.GetChosenOptionInMenuFromUser(6);
                    if (chosenVehicleOpt == 6) break;
                    chosenTypeResult = (eVehicleType) chosenVehicleOpt;
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                    continue;
                }

                closeMenu = true;
            }

            return chosenTypeResult;
        }

        private void ShowFullVehicleData()
        {
            string i_LicenseNumber = InputHandler.GetStringInputFromUser();
            StringBuilder vehicleDetails = m_Garage.ShowVehicleData(i_LicenseNumber);
            PrintHandler.PrintVehicleDetails(vehicleDetails);
        }

        private void chargeElectricVehicle()
        {
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            bool closeMenu = false;
            while (!closeMenu)
            {
                try
                {
                    int amountOfBatteryTimeToAdd = InputHandler.GetIntegerInputFromUser();
                    m_Garage.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                    continue;
                }
                catch (ValueOutOfRangeException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                closeMenu = true;
            }
        }

        private void fuelVehicle()
        {
            bool closeMenu = false;
            string inputLicenseNumber = "\0";
            eFuelType chosenFuelType = eFuelType.None;

            while (!closeMenu)
            {
                PrintHandler.AskForLicenseNumber();
                inputLicenseNumber = InputHandler.GetStringInputFromUser();
                PrintHandler.AskToChooseFuelType();
                chosenFuelType = getFuelTypeAndConvertToEnum();
                if (!chosenFuelType.Equals(eFuelType.None))
                {
                    closeMenu = true;
                }
            }
            closeMenu = false;
            while (!closeMenu)

            {
                try
                {
                    PrintHandler.AskForFuelingAmount();
                    float amountToFuel = InputHandler.GetFloatInputFromUser();
                    m_Garage.FuelVehicle(inputLicenseNumber, chosenFuelType, amountToFuel);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                    continue;
                }
                catch (ValueOutOfRangeException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }

                closeMenu = true;
            }
        }

        private eFuelType getFuelTypeAndConvertToEnum()
        {
            eFuelType typeResult = eFuelType.None;
            bool closeMenu = false;
            while (!closeMenu)
            {
                int chosenFuelType = -1;
                try
                {
                    Menu.ShowFuelTypes();
                    chosenFuelType = InputHandler.GetChosenOptionInMenuFromUser(5);
                    typeResult = (eFuelType) chosenFuelType;
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                    continue;
                }

                closeMenu = true;
            }

            return typeResult;
        }


        private void inflateWheels()
        {
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            try
            {
                m_Garage.InflateToMax(inputLicenseNumber);
            }
            catch (ArgumentException ex)
            {
                PrintHandler.PrintException(ex, ex.Message);
            }

        }

        private void updateVehicleStatus()
        {

            bool closeMenu = false;
            while (!closeMenu)
            {
                int statusChosen;
                string inputLicenseNumber;
                try
                {
                    PrintHandler.AskForLicenseNumber();
                    inputLicenseNumber = InputHandler.GetStringInputFromUser();
                    PrintHandler.AskForVehicleStatus();
                    Menu.ShowUpdatingOptionsByVehicleStatusMenu();
                    statusChosen = InputHandler.GetChosenOptionInMenuFromUser(4);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                    continue;
                }
                if (statusChosen == 4) break;

                m_Garage.UpdateVehicleStatus(inputLicenseNumber, (eVehicleStatus) statusChosen);
                closeMenu = true;
            }


        }

        private void showLicenseNumbersInGarage()
        {
            bool closeMenu = false;
            Console.Clear();
            while (!closeMenu)
            {
                int chosenOpt;
                Menu.ShowFilteringOptionsByVehicleStatusMenu();
                try
                {
                    chosenOpt = InputHandler.GetChosenOptionInMenuFromUser(5);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    continue;
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered");
                    continue;
                }

                if (chosenOpt == 5) break;
                
                List<string> resLicenses = m_Garage.ShowAllVehiclesUnderCare((eVehicleStatus) chosenOpt);

                if (resLicenses.Count > 0)
                {
                    Console.Clear();
                    Menu.ShowFilteringOptionsByVehicleStatusMenu();
                    PrintHandler.PrintList(resLicenses);
                }

                closeMenu = true;

            }

        }
    }
}