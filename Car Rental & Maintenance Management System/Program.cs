using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDesign design = new ConsoleDesign();
            CarRentalSystem system = new CarRentalSystem();

            system.Startup();   

            while (true)   
            {
                design.IntroScreen(); 

                if (!system.Login())  
                    break;            

                MenuUI menu = new MenuUI(system);

                bool logout = false;

                while (!logout)  
                {
                    menu.Run();  

                  
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n1. Logout");
                    Console.WriteLine("2. Exit Program");
                    Console.ResetColor();

                    int choice = system.GetValidators().ReadInt("Choose: ");

                    if (choice == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogged out successfully!");
                        Console.ResetColor();
                        logout = true;  
                    }
                    else if (choice == 2)
                    {
                        system.Shutdown();
                        Environment.Exit(0); 
                    }
                }
            }

            system.Shutdown(); 
        }
    }
}
