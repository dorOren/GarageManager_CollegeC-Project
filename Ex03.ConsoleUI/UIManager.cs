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

                int chosenOpt = InputHandler.GetChosenOptionInMenuFromUser();
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

            eVehicleType vehicleType = getChosenVehicletypeAsEnum();
            string modelName = InputHandler.GetStringInputFromUser();
            string LicenseNumber = InputHandler.GetStringInputFromUser();
            float currentWheelAirPressure = InputHandler.GetFloatInputFromUser();

            if (isVehicleExistsInGarage(LicenseNumber))
            {
                m_Garage.UpdateVehicleStatus(LicenseNumber, eVehicleStatus.BeingRepaired);
                PrintHandler.VehicleIsAlreadyExistsInGarage();
            }
            else
            {
                Vehicle vehicle = m_Garage.InitVehicle(vehicleType);
                if (vehicleType.Equals(eVehicleType.FuelBasedMotorcycle) || vehicleType.Equals(eVehicleType.ElectricMotorcycle))
                {
                    eLicenseType licenseType = getMotorCycleLicenseType();
                    int engineVolume = InputHandler.GetIntegerInputFromUser();
                    m_Garage.SetMotorcycle(vehicle, vehicleType, modelName, LicenseNumber, licenseType, engineVolume);
                }
                else if (vehicleType.Equals(eVehicleType.FuelBasedCar) || vehicleType.Equals(eVehicleType.ElectricCar))
                {
                    eColor chosenColor = getColorFromUser();
                    int numDoors = getNumberOfDoorsFromUSer();
                    GarageManager.SetFuelBasedCar(vehicle, vehicleType, modelName, LicenseNumber, chosenColor, numDoors);
                }
                else if (vehicleType.Equals(eVehicleType.Truck))
                {
                    bool isCarryingDangerousCargo = InputHandler.GetBooleanInputFromUser();
                    float maxCarryingWeight = InputHandler.GetFloatInputFromUser();
                    GarageManager.SetTruck(vehicle, vehicleType, modelName, LicenseNumber, isCarryingDangerousCargo, maxCarryingWeight);
                }
                // TODO: get owner details and send to GarageManager to insert to phonebook.
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
            int chosenType = InputHandler.GetChosenOptionInMenuFromUser();
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

        private eVehicleType getChosenVehicletypeAsEnum()
        { // TODO: Add default value to enum class eVehicleType
            eVehicleType chosenTypeResult = eVehicleType.None;
            PrintHandler.AskToChooseVehicleType();
            Menu.ShowPossibleVehicleTypes();
            int chosenVehicleOpt = InputHandler.GetChosenOptionInMenuFromUser();
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
            List<Vehicle> vehiclesList = GarageManager.GetAllVehiclesInGarage();
            PrintHandler.PrintList(vehiclesList);
        }

        private void chargeElectricVehicle()
        {//TODO: 1. GarageLogic.UpdateVehicleStatus; 2. Insert exceptions; 
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            int amountOfBatteryTimeToAdd = InputHandler.GetIntegerInputFromUser();
            GarageManager.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
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
            GarageManager.FuelVehicle(inputLicenseNumber, chosenFuelType, amountToFuel);
        }

        private eFuelType getFuelTypeAndConvertToEnum()
        {
            int chosenFuelType = InputHandler.GetChosenOptionInMenuFromUser();
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
            GarageManager.InflateToMax(inputLicenseNumber);
        }

        private void updateVehicleStatus()
        {//TODO: 1. GarageLogic.UpdateVehicleStatus; 2. Insert exceptions in switch statement; 
            Console.WriteLine("Enter license number of the vehicle you wish to update:");
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();

            Console.WriteLine("Choose the vehicle's new status: ");
            Menu.ShowFilteringOptionsByVehicleStatusMenu();
            int statusChosen = InputHandler.GetChosenOptionInMenuFromUser();
            switch (statusChosen)
            {
                case 1:
                    GarageManager.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.Paid);
                    break;
                case 2:
                    GarageManager.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.BeingRepaired);
                    break;
                case 3:
                    GarageManager.UpdateVehicleStatus(inputLicenseNumber, eVehicleStatus.Repaired);
                    break;
                case 4:
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }


        }

        private void showLicenseNumbersInGarage()
        {//TODO: add an "any" option to see all vehicles with all statuses ~ Dor
            bool runMenu = true;
            Console.Clear();
            while (runMenu)
            {

                Menu.ShowFilteringOptionsByVehicleStatusMenu();
                int chosenOpt = InputHandler.GetChosenOptionInMenuFromUser();
                List<string> resLicenses = new List<string>();
                switch (chosenOpt)
                {
                    case 1:
                        resLicenses = GarageManager.ShowAllVehiclesUnderCare(eVehicleStatus.Paid);
                        break;
                    case 2:
                        resLicenses = GarageManager.ShowAllVehiclesUnderCare(eVehicleStatus.BeingRepaired);
                        break;
                    case 3:
                        resLicenses = GarageManager.ShowAllVehiclesUnderCare(eVehicleStatus.Repaired);
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