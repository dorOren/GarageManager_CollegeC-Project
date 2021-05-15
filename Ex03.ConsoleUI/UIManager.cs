using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
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
                        //GetNewVehicleDetails();
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

        private void ShowFullVehicleData()
        {// TODO: 1. Maybe should add an exception for returning empty list.
            List<Vehicle> vehiclesList = GarageLogic.GetAllVehiclesInGarage();
            PrintHandler.PrintList(vehiclesList);
        }

        private void chargeElectricVehicle()
        {//TODO: 1. GarageLogic.UpdateVehicleStatus; 2. Insert exceptions; 
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            int amountOfBatteryTimeToAdd = InputHandler.GetIntegerInputFromUser();
            GarageLogic.ChargeBattery(inputLicenseNumber, amountOfBatteryTimeToAdd);
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
            GarageLogic.FuelVehicle(inputLicenseNumber, chosenFuelType, amountToFuel);
        }

        private eFuelType getFuelTypeAndConvertToEnum()
        {
            int chosenFuelType = InputHandler.GetChosenOptionInMenuFromUser();
            eFuelType typeResulet
            switch (chosenFuelType)
            {
                case 1:
                    typeResulet = eFuelType.SOLER;
                    break;
                case 2:
                    typeResulet = eFuelType.OCTAN95;
                    break;
                case 3:
                    typeResulet = eFuelType.OCTAN96;
                    break;
                case 4:
                    typeResulet = eFuelType.OCTAN98;
                    break;
                default:
                    PrintHandler.IllegalOptionOutput();
                    break;
            }

            return typeResulet;
        }


        private void inflateWheels()
        {// TODO: GarageLogic.InflateToMax
            PrintHandler.AskForLicenseNumber();
            string inputLicenseNumber = InputHandler.GetStringInputFromUser();
            GarageLogic.InflateToMax(inputLicenseNumber);
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
                    GarageLogic.UpdateVehicleStatus(inputLicenseNumber, eStatus.PAID);
                    break;
                case 2:
                    GarageLogic.UpdateVehicleStatus(inputLicenseNumber, eStatus.IN_REPAIR);
                    break;
                case 3:
                    GarageLogic.UpdateVehicleStatus(inputLicenseNumber, eStatus.REPAIRED);
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
            Screen.Clear();
            while (runMenu)
            {

                Menu.ShowFilteringOptionsByVehicleStatusMenu();
                int chosenOpt = InputHandler.GetChosenOptionInMenuFromUser();
                List<string> resLicenses = new List<string>();
                switch (chosenOpt)
                {
                    case 1:
                        resLicenses = GarageLogic.RequestVechilesByStatus(eStatus.PAID);
                        break;
                    case 2:
                        resLicenses = GarageLogic.RequestVechilesByStatus(eStatus.IN_REPAIR);
                        break;
                    case 3:
                        resLicenses = GarageLogic.RequestVechilesByStatus(eStatus.REPAIRED);
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
                    Screen.Clear();
                    Menu.ShowFilteringOptionsByVehicleStatusMenu();
                    PrintHandler.PrintList(resLicenses);
                }

            }

        }
    }
}