using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    internal class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 

        public Admin() { }

        public Admin(int id, string username, string password)
        {
            AdminId = id;
            Username = username;
            Password = password;
        }

        public string ToCsv()
        {
            return AdminId + "," + Username + "," + Password;
        }
    }
}

