using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class ConsoleDesign
    {
        public void IntroScreen()
        {
            Console.Clear();
            Console.Title = "Car Rental & Maintenance System";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================================");
            Console.WriteLine("      CAR RENTAL & MAINTENANCE MANAGEMENT SYSTEM    ");
            Console.WriteLine("===================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Rent Cars");
            Console.WriteLine("  Return Cars");
            Console.WriteLine("  Maintenance Logging");
            Console.WriteLine("  Conflict Detection");
            Console.WriteLine("  CSV File Storage");
            Console.WriteLine("  POS Receipt Printing (Text File)");
            Console.WriteLine("  Admin Login");
            Console.WriteLine("  Reports");
            Console.ResetColor();

            Console.WriteLine("\nLoading system...");
            Thread.Sleep(900);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("System Ready! Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public void PrintHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================================");
            Console.WriteLine(" " + title.ToUpper());
            Console.WriteLine("===================================================");
            Console.ResetColor();
        }

        public void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" " + message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" " + message);
            Console.ResetColor();
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" " + message);
            Console.ResetColor();
        }

        public void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public void PrintMenu(string title, string[] options)
        {
            PrintHeader(title); // Use the existing header design

            for (int i = 0; i < options.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{i + 1}. {options[i]}");
                Console.ResetColor();
            }
        }

    }
}
