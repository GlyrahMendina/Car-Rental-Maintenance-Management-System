using System;
using System.Threading;

namespace Car_Rental___Maintenance_Management_System
{
    public class MenuUI
    {
        public MenuUI() { }
        private CarRentalSystem system;
        private Validator validator;
        private ConsoleDesign design;

        public MenuUI(CarRentalSystem sys)
        {
            system = sys;
            validator = system.GetValidators();
            design = new ConsoleDesign();
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                string[] options = new string[]
                {
                    "View All Cars",
                    "View Available Cars",
                    "Search Car",
                    "Rent a Car",
                    "Return a Car",
                    "Log Maintenance",
                    "Finish Maintenance",
                    "View Rentals",
                    "View Maintenance Records",
                    "Daily Report",
                    "Exit"
                };

                design.PrintMenu("MAIN MENU", options);

                int choice = validator.ReadInt("\nChoose: ");

                switch (choice)
                {
                    case 1: system.ShowAllCars(); break;
                    case 2: system.ShowAvailableCars(); break;
                    case 3: system.SearchCar(); break;
                    case 4: system.RentCar(); break;
                    case 5: system.ReturnCar(); break;
                    case 6: system.LogMaintenance(); break;
                    case 7: system.FinishMaintenance(); break;
                    case 8: system.ShowRentals(); break;
                    case 9: system.ShowMaintenanceRecords(); break;
                    case 10: system.DailyReport(); break;
                    case 11:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Saving data... Goodbye!");
                        Console.ResetColor();
                        running = false;
                        break;
                    default:
                        design.Error("Invalid choice.");
                        break;
                }

                if (running) design.Pause();
            }
        }
    }
}
