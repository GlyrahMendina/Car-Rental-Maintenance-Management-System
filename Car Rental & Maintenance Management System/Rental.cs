using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string StartDate { get; set; } // YYYY-MM-DD
        public string EndDate { get; set; }
        public string Status { get; set; } // Ongoing, Returned

        public Rental() { }

        public Rental(int id, int carId, int customerId, string start, string end, string status)
        {
            RentalId = id;
            CarId = carId;
            CustomerId = customerId;
            StartDate = start;
            EndDate = end;
            Status = status;
        }

        public string ToCsv()
        {
            return RentalId + "," + CarId + "," + CustomerId + "," + StartDate + "," + EndDate + "," + Status;
        }
    }
}
