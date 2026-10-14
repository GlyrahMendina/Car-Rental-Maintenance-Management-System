using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }

        public Customer() { }

        public Customer(int id, string name, string contact)
        {
            CustomerId = id;
            FullName = name;
            ContactNumber = contact;
        }

        public string ToCsv()
        {
            return CustomerId + "," + FullName + "," + ContactNumber;
        }
    }
}
