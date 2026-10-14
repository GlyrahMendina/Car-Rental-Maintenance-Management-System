using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class AuthManager
    {
        private ConsoleDesign design;

        private string adminUser = "admin";
        private string adminPass = "admin123";

        public AuthManager(ConsoleDesign d)
        {
            design = d;
        }

        public bool Login()
        {
            design.PrintHeader("Admin Login");

            int tries = 3;

            while (tries > 0)
            {
                Console.Write("Username: ");
                string user = Console.ReadLine();

                Console.Write("Password: ");
                string pass = ReadPassword();

                if (IsValidCredential(user, pass))
                {
                    design.Success("Login successful!");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    tries--;
                    design.Error("Invalid login. Attempts left: " + tries);
                    Console.ReadKey();
                }
            }

            design.Error("Too many failed attempts. Access denied.");
            Console.ReadKey();
            return false;
        }

        private bool IsValidCredential(string user, string pass)
        {
            if (user == null) user = "";
            if (pass == null) pass = "";

            user = user.Trim();

            if (user == "" || pass == "")
            {
                return false;
            }

            return user == adminUser && pass == adminPass;
        }

        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    if (password.Length < 20)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                }
            }

            return password;
        }
    }
}
