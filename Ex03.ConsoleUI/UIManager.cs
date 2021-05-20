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
            PrintHandler.AskForLicenseNumber();
            string licenseNumber = InputHandler.GetStringInputFromUser();
            try
            {
                if (isVehicleExistsInGarage(licenseNumber))
                {
                    Console.WriteLine("Log: Vehicle exists in garage");
                    m_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.BeingRepaired);
                    PrintHandler.VehicleIsAlreadyExistsInGarage();
                }
                else
                {
                    Console.WriteLine("Log: Vehicle doesn't exists in garage.");
                    eVehicleType vehicleType = getChosenVehicleTypeAsEnum();
                    if (!vehicleType.Equals(eVehicleType.None))
                    {
                        Vehicle vehicle = m_Garage.InitVehicle(vehicleType);
                        PrintHandler.AskForVehicleModel();
                        string modelName = InputHandler.GetStringInputFromUser();
                        bool valid = false;
                        float currentWheelAirPressure = 0;
                        while (!valid)
                        {
                            try
                            {
                                PrintHandler.AskForCurrentWheelAirPressure();
                                currentWheelAirPressure = InputHandler.GetFloatInputFromUser();
                                valid = true;
                            }
                            catch (ArgumentException ex)
                            {
                                PrintHandler.PrintException(ex, ex.Message);
                            }
                            catch (FormatException ex)
                            {
                                PrintHandler.PrintException(ex, "Illegal input entered.");
                            }
                        }

                        PrintHandler.AskForWheelManufacturer();
                        string wheelManufacturer = InputHandler.GetStringInputFromUser();
                        if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle) ||
                            vehicleType.Equals(eVehicleType.ElectricMotorcycle))
                        {
                            getNewMotorcycleDetails(vehicle, vehicleType, modelName, licenseNumber, wheelManufacturer,
                                currentWheelAirPressure);
                        }
                        else if (vehicleType.Equals(eVehicleType.FuelBasedCar) ||
                                 vehicleType.Equals(eVehicleType.ElectricCar))
                        {
                            getNewCarDetails(vehicle, vehicleType, modelName, licenseNumber, wheelManufacturer,
                                currentWheelAirPressure);
                        }
                        else if (vehicleType.Equals(eVehicleType.Truck))
                        {
                            getNewTruckDetails(vehicle, modelName, licenseNumber, wheelManufacturer,
                                currentWheelAirPressure);
                        }

                        m_Garage.AddToVehiclesListDB(vehicle);
                    }
                }
            }
            catch(Exception ex)
            {
                PrintHandler.PrintException(ex,ex.Message);
            }

        }

        private void getNewMotorcycleDetails(Vehicle io_Vehicle, eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturer, float i_CurrentWheelAirPressure)
        {
            eLicenseType licenseType = getMotorCycleLicenseType();
            if (!licenseType.Equals(eLicenseType.None))
            {
                int engineVolume = 0;
                bool valid = false;
                while (!valid)
                {
                    try
                    {
                        PrintHandler.AskForEngineVolume();
                        engineVolume = InputHandler.GetIntegerInputFromUser();
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

                    valid = true;
                }

                if (i_VehicleType.Equals(eVehicleType.FuelBasedMotorcycle))
                {
                    valid = false;
                    float currentFuelAmount = 0;
                    while (!valid)
                    {
                        try
                        {
                            PrintHandler.AskForCurrentFuelAmount();
                            currentFuelAmount = InputHandler.GetFloatInputFromUser();
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

                        valid = true;
                    }

                    (io_Vehicle as FuelBasedMotorcycle).SetFields(i_ModelName, i_LicenseNumber, currentFuelAmount,
                        licenseType, engineVolume, i_WheelManufacturer, i_CurrentWheelAirPressure);

                }
                else if (i_VehicleType.Equals(eVehicleType.ElectricMotorcycle))
                {
                    valid = false;
                    float currentBatteryTime = 0;
                    while (!valid)
                    {
                        try
                        {
                            PrintHandler.AskForCurrentBatteryTime();
                            currentBatteryTime = InputHandler.GetFloatInputFromUser();
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

                        valid = true;
                    }

                    (io_Vehicle as ElectricMotorcycle).SetFields(i_ModelName, i_LicenseNumber, currentBatteryTime,
                        licenseType, engineVolume, i_WheelManufacturer, i_CurrentWheelAirPressure);

                }
                getCustomerDetails(i_LicenseNumber);
                
            }
        }

        private void getNewCarDetails(Vehicle io_Vehicle, eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturer, float i_CurrentWheelAirPressure)
        {
            eColor chosenColor = getColorFromUser();
            if (!chosenColor.Equals(eColor.None))
            {
                int numDoors = getNumberOfDoorsFromUSer();
                if (i_VehicleType.Equals(eVehicleType.FuelBasedCar))
                {
                    bool valid = false;
                    float currentFuelAmount = 0;
                    while (!valid)
                    {
                        try
                        {
                            PrintHandler.AskForCurrentFuelAmount();
                            currentFuelAmount = InputHandler.GetFloatInputFromUser();
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

                        valid = true;
                    }

                    (io_Vehicle as FuelBasedCar).SetFields(i_ModelName, i_LicenseNumber, currentFuelAmount,
                        chosenColor,
                        numDoors, i_WheelManufacturer, i_CurrentWheelAirPressure);
                }
                else if (i_VehicleType.Equals(eVehicleType.ElectricCar))
                {
                    bool valid = false;
                    float currentBatteryTime = 0;
                    while (!valid)
                    {
                        try
                        {
                            PrintHandler.AskForCurrentBatteryTime();
                            currentBatteryTime = InputHandler.GetFloatInputFromUser();
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

                        valid = true;
                    }

                    (io_Vehicle as ElectricCar).SetFields(i_ModelName, i_LicenseNumber, currentBatteryTime,
                        chosenColor,
                        numDoors, i_WheelManufacturer, i_CurrentWheelAirPressure);
                }
                getCustomerDetails(i_LicenseNumber);
            }
        }

        private void getNewTruckDetails(Vehicle io_Vehicle, string i_ModelName, string i_LicenseNumber,
            string i_WheelManufacturer, float i_CurrentWheelAirPressure)
        {
            bool isCarryingDangerousCargo = isTruckCarryingDangerousCargo();
            bool valid = false;
            float maxCarryingWeight = 0;
            float currentFuelAmount = 0;
            while (!valid)
            {
                try
                {
                    PrintHandler.AskForMaxCarryingWeight();
                    maxCarryingWeight = InputHandler.GetFloatInputFromUser();
                    PrintHandler.AskForCurrentFuelAmount();
                    currentFuelAmount = InputHandler.GetFloatInputFromUser();
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

                valid = true;
            }

            (io_Vehicle as Truck).SetFields(i_ModelName, i_LicenseNumber, currentFuelAmount,
                isCarryingDangerousCargo,
                maxCarryingWeight, i_WheelManufacturer, i_CurrentWheelAirPressure);

            getCustomerDetails(i_LicenseNumber);
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
            bool res = InputHandler.GetBooleanInputFromUser();
            return res;
        }

        private bool isVehicleExistsInGarage(string i_LicenseNumber)
        {
            bool found = false;
            try
            {
                found = m_Garage.FindVehicleInGarage(i_LicenseNumber) != null;
            }
            catch { }

            return found;
        }

        private eLicenseType getMotorCycleLicenseType()
        {
            bool closeMenu = false;
            eLicenseType resLicenseType=eLicenseType.None;

            while (!closeMenu)
            {
                int chosenType;
                try
                {
                    PrintHandler.AskForMotorcycleLicenseType();
                    Menu.ShowMotorcycleLicenseTypes();
                    chosenType = InputHandler.GetChosenOptionInMenuFromUser(5);
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

                if (chosenType == 5) break;
                resLicenseType = (eLicenseType) chosenType;
                closeMenu = true;
            }


            return resLicenseType;
        }

        private int getNumberOfDoorsFromUSer()
        {
            int res = 0;
            bool valid = false;
            while (!valid)
            {
                try
                {
                    PrintHandler.AskForNumberOfDoors();
                    res = InputHandler.GetIntegerInputFromUser();
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
                valid = res >= 2 && res <= 5;
            }

            return res;
        }

        private eColor getColorFromUser()
        {
            bool closeMenu = false;
            eColor resColor = eColor.None;
            while (!closeMenu)
            {
                int chosenOpt;
                try
                {
                    PrintHandler.AskForColor();
                    Menu.ShowColorsOptionsForCars();
                    chosenOpt = InputHandler.GetChosenOptionInMenuFromUser(5);
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

                if (chosenOpt == 5) break;
                resColor = (eColor) chosenOpt;
                closeMenu = true;
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
            PrintHandler.AskForLicenseNumber();
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
                    PrintHandler.AskForAmountOfBatteryInMinutesToCharge();
                    int amountOfBatteryTimeToAdd = InputHandler.GetIntegerInputFromUser();
                    m_Garage.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                    
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
                PrintHandler.AskForLicenseNumber();
                string inputLicenseNumber = InputHandler.GetStringInputFromUser();
                try
                {

                    PrintHandler.AskForVehicleStatus();
                    Menu.ShowUpdatingOptionsByVehicleStatusMenu();
                    statusChosen = InputHandler.GetChosenOptionInMenuFromUser(4);
                    if (statusChosen == 4) break;
                    m_Garage.UpdateVehicleStatus(inputLicenseNumber, (eVehicleStatus)statusChosen);
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    PrintHandler.PrintException(ex, "Illegal input entered.");
                }
                

                //m_Garage.UpdateVehicleStatus(inputLicenseNumber, (eVehicleStatus) statusChosen);
                //closeMenu = true;
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
                else
                {
                    PrintHandler.NoMatchingVehiclesInGarage();
                }


                closeMenu = true;

            }

        }
    }
}