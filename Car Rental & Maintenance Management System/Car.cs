using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class Car
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Status { get; set; } // Available, Rented, Maintenance

        public Car() { }

        public Car(int id, string plate, string brand, string model, int year, string status)
        {
            CarId = id;
            PlateNumber = plate;
            Brand = brand;
            Model = model;
            Year = year;
            Status = status;
        }

        public string ToCsv()
        {
            return CarId + "," + PlateNumber + "," + Brand + "," + Model + "," + Year + "," + Status;
        }
    }
}
