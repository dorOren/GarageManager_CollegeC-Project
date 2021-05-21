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
        private PrintHandler m_PrintHandler;
        private InputHandler m_InputHandler;

        public UIManager()
        {
            m_Garage = new GarageManager();
            m_PrintHandler = new PrintHandler();
            m_InputHandler = new InputHandler();
        }

        public void Start()
        {
            bool runGarage = true;
            while (runGarage)
            {
                Menu.ShowMainMenu();

                int chosenOpt = m_InputHandler.GetChosenOptionInMenuFromUser(8);
                Console.Clear();
                switch (chosenOpt)
                {
                    case 1:
                        getNewVehicleDetails();
                        break;
                    case 2:
                        showLicenseNumbersInGarage();
                        break;
                    case 3:
                        updateVehicleStatus();
                        break;
                    case 4:
                        inflateWheels();
                        break;
                    case 5:
                        fuelVehicle();
                        break;
                    case 6:
                        chargeElectricVehicle();
                        break;
                    case 7:
                        ShowFullVehicleData();
                        break;
                    case 8:
                        runGarage = false;
                        break;
                    default:
                        m_PrintHandler.IllegalOptionOutput();
                        break;

                }
            }

        }


        private void getNewVehicleDetails()
        {
            m_PrintHandler.AskForLicenseNumber();
            string licenseNumber = m_InputHandler.GetStringInputFromUser();
            try
            {
                if (isVehicleExistsInGarage(licenseNumber))
                {
                    Console.WriteLine("Log: Vehicle exists in garage");
                    m_Garage.UpdateVehicleStatus(licenseNumber, eVehicleStatus.BeingRepaired);
                    m_PrintHandler.VehicleIsAlreadyExistsInGarage();
                }
                else
                {
                    Console.WriteLine("Log: Vehicle doesn't exists in garage.");
                    eVehicleType vehicleType = getChosenVehicleTypeAsEnum();
                    if (!vehicleType.Equals(eVehicleType.None))
                    {
                        Vehicle vehicle = m_Garage.InitVehicle(vehicleType);
                        m_PrintHandler.AskForVehicleModel();
                        string modelName = m_InputHandler.GetStringInputFromUser();
                        bool valid = false;
                        float currentWheelAirPressure = 0;
                        while (!valid)
                        {
                            try
                            {
                                m_PrintHandler.AskForCurrentWheelAirPressure();
                                currentWheelAirPressure = m_InputHandler.GetFloatInputFromUser();
                                valid = true;
                            }
                            catch (ArgumentException ex)
                            {
                                m_PrintHandler.PrintException(ex, ex.Message);
                            }
                            catch (FormatException ex)
                            {
                                m_PrintHandler.PrintException(ex, "Illegal input entered.");
                            }
                        }

                        m_PrintHandler.AskForWheelManufacturer();
                        string wheelManufacturer = m_InputHandler.GetStringInputFromUser();
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
                m_PrintHandler.PrintException(ex,ex.Message);
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
                        m_PrintHandler.AskForEngineVolume();
                        engineVolume = m_InputHandler.GetIntegerInputFromUser();

                        valid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        m_PrintHandler.PrintException(ex, ex.Message);
                    }
                    catch (FormatException ex)
                    {
                        m_PrintHandler.PrintException(ex, "Illegal input entered.");
                    }
                }

                if (i_VehicleType.Equals(eVehicleType.FuelBasedMotorcycle))
                {
                    valid = false;
                    float currentFuelAmount = 0;
                    while (!valid)
                    {
                        try
                        {
                            m_PrintHandler.AskForCurrentFuelAmount();
                            currentFuelAmount = m_InputHandler.GetFloatInputFromUser();
                            valid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            m_PrintHandler.PrintException(ex, ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            m_PrintHandler.PrintException(ex, "Illegal input entered.");
                        }
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
                            m_PrintHandler.AskForCurrentBatteryTime();
                            currentBatteryTime = m_InputHandler.GetFloatInputFromUser();
                            valid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            m_PrintHandler.PrintException(ex, ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            m_PrintHandler.PrintException(ex, "Illegal input entered.");
                        }
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
                            m_PrintHandler.AskForCurrentFuelAmount();
                            currentFuelAmount = m_InputHandler.GetFloatInputFromUser();
                            valid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            m_PrintHandler.PrintException(ex, ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            m_PrintHandler.PrintException(ex, "Illegal input entered.");
                        }
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
                            m_PrintHandler.AskForCurrentBatteryTime();
                            currentBatteryTime = m_InputHandler.GetFloatInputFromUser();
                            valid = true;
                        }
                        catch (ArgumentException ex)
                        {
                            m_PrintHandler.PrintException(ex, ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            m_PrintHandler.PrintException(ex, "Illegal input entered.");
                        }
                    }

                    (io_Vehicle as ElectricCar).SetFields(i_ModelName, i_LicenseNumber, currentBatteryTime,
                        chosenColor, numDoors, i_WheelManufacturer, i_CurrentWheelAirPressure);
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
                    m_PrintHandler.AskForMaxCarryingWeight();
                    maxCarryingWeight = m_InputHandler.GetFloatInputFromUser();
                    m_PrintHandler.AskForCurrentFuelAmount();
                    currentFuelAmount = m_InputHandler.GetFloatInputFromUser();
                    valid = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
            }

            (io_Vehicle as Truck).SetFields(i_ModelName, i_LicenseNumber, currentFuelAmount,
                isCarryingDangerousCargo, maxCarryingWeight, i_WheelManufacturer, i_CurrentWheelAirPressure);

            getCustomerDetails(i_LicenseNumber);
        }

        private void getCustomerDetails(string i_LicenseNumber)
        {
            m_PrintHandler.AskForOwnerName();
            string ownerName = m_InputHandler.GetStringInputFromUser();
            bool valid = false;
            string ownerPhone = "\0";
            while (!valid)
            {
                m_PrintHandler.AskForOwnerPhone();
                ownerPhone = m_InputHandler.GetStringInputFromUser();
                valid = (int.TryParse(ownerPhone, out int temp) && (ownerPhone.Length > 7) && (ownerPhone.Length < 11));
            }

            m_Garage.AddCustomerDetailsToBook(ownerName, ownerPhone, i_LicenseNumber);
        }

        private bool isTruckCarryingDangerousCargo()
        {
            m_PrintHandler.AskIfTruckIsCarryingDangerousCargo();
            bool res = m_InputHandler.GetBooleanInputFromUser();
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
                    m_PrintHandler.AskForMotorcycleLicenseType();
                    Menu.ShowMotorcycleLicenseTypes();
                    chosenType = m_InputHandler.GetChosenOptionInMenuFromUser(5);
                    if (chosenType == 5) break;
                    resLicenseType = (eLicenseType)chosenType;
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
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
                    m_PrintHandler.AskForNumberOfDoors();
                    res = m_InputHandler.GetIntegerInputFromUser();
                    valid = res >= 2 && res <= 5;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
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
                    m_PrintHandler.AskForColor();
                    Menu.ShowColorsOptionsForCars();
                    chosenOpt = m_InputHandler.GetChosenOptionInMenuFromUser(5);
                    if (chosenOpt == 5) break;
                    resColor = (eColor)chosenOpt;
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
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
                    m_PrintHandler.AskToChooseVehicleType();
                    Menu.ShowPossibleVehicleTypes();
                    int chosenVehicleOpt = m_InputHandler.GetChosenOptionInMenuFromUser(6);
                    if (chosenVehicleOpt == 6) break;
                    chosenTypeResult = (eVehicleType) chosenVehicleOpt;
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
            }

            return chosenTypeResult;
        }

        private void ShowFullVehicleData()
        {
            m_PrintHandler.AskForLicenseNumber();
            string i_LicenseNumber = m_InputHandler.GetStringInputFromUser();
            StringBuilder vehicleDetails = m_Garage.ShowVehicleData(i_LicenseNumber);

            m_PrintHandler.PrintVehicleDetails(vehicleDetails);
        }

        private void chargeElectricVehicle()
        {
            m_PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = m_InputHandler.GetStringInputFromUser();
            bool closeMenu = false;
            while (!closeMenu)
            {
                try
                {
                    m_PrintHandler.AskForAmountOfBatteryInMinutesToCharge();
                    int amountOfBatteryTimeToAdd = m_InputHandler.GetIntegerInputFromUser();
                    m_Garage.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
            }
        }

        private void fuelVehicle()
        {
            bool closeMenu = false;
            string inputLicenseNumber = "\0";
            eFuelType chosenFuelType = eFuelType.None;

            while (!closeMenu)
            {
                m_PrintHandler.AskForLicenseNumber();
                inputLicenseNumber = m_InputHandler.GetStringInputFromUser();
                m_PrintHandler.AskToChooseFuelType();
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
                    m_PrintHandler.AskForFuelingAmount();
                    float amountToFuel = m_InputHandler.GetFloatInputFromUser();
                    m_Garage.FuelVehicle(inputLicenseNumber, chosenFuelType, amountToFuel);
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
                catch (ValueOutOfRangeException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
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
                    chosenFuelType = m_InputHandler.GetChosenOptionInMenuFromUser(5);
                    typeResult = (eFuelType) chosenFuelType;
                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered.");
                }
            }

            return typeResult;
        }


        private void inflateWheels()
        {
            m_PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = m_InputHandler.GetStringInputFromUser();
            try
            {
                m_Garage.InflateToMax(inputLicenseNumber);
            }
            catch (ArgumentException ex)
            {
                m_PrintHandler.PrintException(ex, ex.Message);
            }

        }

        private void updateVehicleStatus()
        {

            bool closeMenu = false;
            while (!closeMenu)
            {
                m_PrintHandler.AskForLicenseNumber();
                string inputLicenseNumber = m_InputHandler.GetStringInputFromUser();
                if (isVehicleExistsInGarage(inputLicenseNumber))
                {
                    try
                    {

                        m_PrintHandler.AskForVehicleStatus();
                        Menu.ShowUpdatingOptionsByVehicleStatusMenu();
                        int statusChosen = m_InputHandler.GetChosenOptionInMenuFromUser(4);
                        if (statusChosen == 4) break;
                        m_Garage.UpdateVehicleStatus(inputLicenseNumber, (eVehicleStatus) statusChosen);
                        closeMenu = true;
                    }
                    catch (ArgumentException ex)
                    {
                        m_PrintHandler.PrintException(ex, ex.Message);
                    }
                    catch (FormatException ex)
                    {
                        m_PrintHandler.PrintException(ex, "Illegal input entered.");
                    }
                }
                else
                {
                    m_PrintHandler.NoMatchingVehiclesInGarage();
                }

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
                    chosenOpt = m_InputHandler.GetChosenOptionInMenuFromUser(5);
                    if (chosenOpt == 5) break;
                    List<string> resLicenses = new List<string>();
                    resLicenses = m_Garage.ShowAllVehiclesUnderCare((eVehicleStatus)chosenOpt);

                    if (resLicenses.Count > 0)
                    {
                        Console.Clear();
                        Menu.ShowFilteringOptionsByVehicleStatusMenu();
                        m_PrintHandler.PrintList(resLicenses);
                    }
                    else
                    {
                        m_PrintHandler.NoMatchingVehiclesInGarage();
                    }

                    closeMenu = true;
                }
                catch (ArgumentException ex)
                {
                    m_PrintHandler.PrintException(ex, ex.Message);
                }
                catch (FormatException ex)
                {
                    m_PrintHandler.PrintException(ex, "Illegal input entered");
                }

            }

        }
    }
}