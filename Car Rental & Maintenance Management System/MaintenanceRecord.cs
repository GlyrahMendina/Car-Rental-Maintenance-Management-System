using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class MaintenanceRecord
    {
        public int MaintenanceId { get; set; }
        public int CarId { get; set; }
        public string PerformedBy { get; set; }
        public string DateCompleted { get; set; }
        public string Details { get; set; }

        public MaintenanceRecord() { }

        public MaintenanceRecord(int id, int carId, string by, string date, string details)
        {
            MaintenanceId = id;
            CarId = carId;
            PerformedBy = by;
            DateCompleted = date;
            Details = details;
        }

        public string ToCsv()
        {
            return MaintenanceId + "," + CarId + "," + PerformedBy + "," + DateCompleted + "," + Details;
        }
    }
}
