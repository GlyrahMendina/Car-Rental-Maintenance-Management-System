using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    public class Validator
    {
        public int ReadInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();

                if (input == null) input = "";
                input = input.Trim();

                int value;
                if (int.TryParse(input, out value))
                {
                    return value;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number. Try again.");
                Console.ResetColor();
            }
        }

        public string ReadNonEmpty(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();

                if (input == null) input = "";
                input = input.Trim();

                if (input.Length > 0)
                {
                    return input;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input cannot be empty.");
                Console.ResetColor();
            }
        }

        public string ReadContactNumber(string label)
        {
            while (true)
            {
                string contact = ReadNonEmpty(label);

                bool allDigits = true;
                for (int i = 0; i < contact.Length; i++)
                {
                    if (contact[i] < '0' || contact[i] > '9')
                    {
                        allDigits = false;
                        break;
                    }
                }

                if (allDigits && contact.Length >= 10 && contact.Length <= 11)
                {
                    return contact;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Contact must be 10-11 digits only.");
                Console.ResetColor();
            }
        }

        public DateTime ReadDate(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine();

                if (input == null) input = "";
                input = input.Trim();

                DateTime date;
                if (DateTime.TryParse(input, out date))
                {
                    return date.Date;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Example: 2026-01-30");
                Console.ResetColor();
            }
        }

        public void PressEnterToContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
