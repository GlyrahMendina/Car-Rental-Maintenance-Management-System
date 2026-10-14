using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Car_Rental___Maintenance_Management_System
{
    public class CarRentalSystem
    {
        private List<Car> cars;
        private List<Customer> customers;
        private List<Rental> rentals;
        private List<MaintenanceRecord> maintenanceRecords;
        private List<Admin> admins;

        private FileManager fileManager;
        private Validator validator;
        private ReceiptPrinter receiptPrinter;

        private string receiptsFolder => Path.Combine(dataFolder, "Receipts");
        private string logsFolder => Path.Combine(dataFolder, "Logs");
        private string backupsFolder => Path.Combine(dataFolder, "Backups");


        private string dataFolder = @"C:\Users\Glyrah\Downloads\Data";
        private string carsFile => Path.Combine(dataFolder, "cars.csv");
        private string customersFile => Path.Combine(dataFolder, "customers.csv");
        private string rentalsFile => Path.Combine(dataFolder, "rentals.csv");
        private string maintenanceFile => Path.Combine(dataFolder, "maintenance.csv");
        private string adminsFile => Path.Combine(dataFolder, "admins.csv");

        public CarRentalSystem()
        {
            cars = new List<Car>();
            customers = new List<Customer>();
            rentals = new List<Rental>();
            maintenanceRecords = new List<MaintenanceRecord>();
            admins = new List<Admin>();

            fileManager = new FileManager();
            validator = new Validator();
            receiptPrinter = new ReceiptPrinter();
        }

        public Validator GetValidators() => validator;

        // ===================== STARTUP / SHUTDOWN =====================
        public void Startup()
        {
            fileManager.EnsureFolder(dataFolder);
            CreateDefaultFilesIfMissing();
            LoadData();
        }

        public void Shutdown() => SaveData();

        private void CreateDefaultFilesIfMissing()
        {
            fileManager.CreateFileIfMissing(carsFile, new List<string>()
            {
                "CarId,PlateNumber,Brand,Model,Year,Status",
                "1,ABC123,Toyota,Vios,2020,Available",
                "2,XYZ999,Honda,Civic,2019,Rented",
                "3,QWE456,Ford,Ranger,2021,Maintenance"
            });

            fileManager.CreateFileIfMissing(customersFile, new List<string>()
            {
                "CustomerId,FullName,ContactNumber",
                "1,Juan Dela Cruz,09123456789"
            });

            fileManager.CreateFileIfMissing(rentalsFile, new List<string>()
            {
                "RentalId,CarId,CustomerId,StartDate,EndDate,Status",
                "1,2,1,2026-01-25,2026-01-30,Ongoing"
            });

            fileManager.CreateFileIfMissing(maintenanceFile, new List<string>()
            {
                "MaintenanceId,CarId,PerformedBy,DateCompleted,Details",
                "1,3,Mechanic A,2026-01-15,Oil change"
            });

            fileManager.CreateFileIfMissing(adminsFile, new List<string>()
            {
                "AdminId,Username,Password",
                "1,admin,admin123",
                "2,master,master123"
            });
        }

        public void LoadData()
        {
            LoadCars();
            LoadCustomers();
            LoadRentals();
            LoadMaintenance();
            LoadAdmins();
        }

        public void SaveData()
        {
            SaveCars();
            SaveCustomers();
            SaveRentals();
            SaveMaintenance();
            SaveAdmins();
        }

        // ===================== LOGIN =====================
        public bool Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== ADMIN LOGIN ===");
            Console.ResetColor();

            int attempts = 3;

            while (attempts > 0)
            {
                string username = validator.ReadNonEmpty("Username: ");
                string password = validator.ReadNonEmpty("Password: ");

                Admin admin = GetAdmin(username, password);

                if (admin != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login successful! Welcome, " + admin.Username + "!");
                    Console.ResetColor();
                    return true;
                }

                attempts--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid username or password. Attempts left: " + attempts);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Login failed. Exiting system.");
            Console.ResetColor();
            return false;
        }

        private Admin GetAdmin(string username, string password)
        {
            return admins.FirstOrDefault(a => a.Username == username && a.Password == password);
        }

        // ===================== SHOW TABLES =====================
        public void ShowAllCars()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== ALL CARS ===");
            Console.ResetColor();

            Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6} {5,-12}",
                "CarId", "Brand", "PlateNumber", "Model", "Year", "Status");

            foreach (var c in cars)
            {
                Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6} {5,-12}",
                    c.CarId, c.Brand, c.PlateNumber, c.Model, c.Year, c.Status);
            }
        }

        public void ShowAvailableCars()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=== AVAILABLE CARS ===");
            Console.ResetColor();

            Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6}",
                "CarId", "Brand", "PlateNumber", "Model", "Year");

            bool found = false;
            foreach (var c in cars)
            {
                if (c.Status == "Available")
                {
                    found = true;
                    Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6}",
                        c.CarId, c.Brand, c.PlateNumber, c.Model, c.Year);
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available cars right now.");
                Console.ResetColor();
            }
        }

        public void ShowRentals()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== RENTAL RECORDS ===");
            Console.ResetColor();

            Console.WriteLine("{0,-8} {1,-6} {2,-6} {3,-12} {4,-12} {5,-10}",
                "RentalID", "CarID", "CustID", "StartDate", "EndDate", "Status");

            foreach (var r in rentals)
            {
                Console.WriteLine("{0,-8} {1,-6} {2,-6} {3,-12} {4,-12} {5,-10}",
                    r.RentalId, r.CarId, r.CustomerId, r.StartDate, r.EndDate, r.Status);
            }
        }

        public void ShowMaintenanceRecords()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== MAINTENANCE RECORDS ===");
            Console.ResetColor();

            Console.WriteLine("{0,-8} {1,-6} {2,-12} {3,-12} {4,-30}",
                "MaintID", "CarID", "PerformedBy", "Date", "Details");

            foreach (var m in maintenanceRecords)
            {
                Console.WriteLine("{0,-8} {1,-6} {2,-12} {3,-12} {4,-30}",
                    m.MaintenanceId, m.CarId, m.PerformedBy, m.DateCompleted, m.Details);
            }
        }

        // ===================== SEARCH =====================
        public void SearchCar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== SEARCH CAR (Brand/Model) ===");
            Console.ResetColor();

            string keyword = validator.ReadNonEmpty("Enter keyword: ").ToLower();
            bool found = false;

            Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6} {5,-12}",
                "CarId", "Brand", "PlateNumber", "Model", "Year", "Status");

            foreach (var c in cars)
            {
                if (c.Brand.ToLower().Contains(keyword) || c.Model.ToLower().Contains(keyword))
                {
                    found = true;
                    Console.WriteLine("{0,-6} {1,-10} {2,-12} {3,-12} {4,-6} {5,-12}",
                        c.CarId, c.Brand, c.PlateNumber, c.Model, c.Year, c.Status);
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No car found with keyword: " + keyword);
                Console.ResetColor();
            }
        }

        // ===================== RENT =====================
        public void RentCar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== RENT A CAR ===");
            Console.ResetColor();

            ShowAvailableCars();

            int carId = validator.ReadInt("Enter Car ID to rent: ");
            Car car = GetCarById(carId);

            if (car == null || car.Status != "Available")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Car not available or not found.");
                Console.ResetColor();
                return;
            }

            string name = validator.ReadNonEmpty("Customer Full Name: ");
            string contact = validator.ReadContactNumber("Customer Contact Number: ");

            DateTime start = DateTime.Today;
            Console.WriteLine("Start Date: " + start.ToString("yyyy-MM-dd"));

            DateTime end = validator.ReadDate("End Date (YYYY-MM-DD): ");
            if (end < start)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("End date cannot be earlier than start date.");
                Console.ResetColor();
                return;
            }

            if (HasDateConflict(carId, start, end))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Conflict: Car already booked during that date range.");
                Console.ResetColor();
                return;
            }

            int customerId = AddCustomer(name, contact);
            int rentalId = GetNextRentalId();
            Rental rental = new Rental(rentalId, carId, customerId, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"), "Ongoing");
            rentals.Add(rental);

            car.Status = "Rented";
            PrintAndSaveReceipt(rental);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rental confirmed!");
            Console.ResetColor();
        }

        // ===================== RETURN =====================
        public void ReturnCar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== RETURN A CAR ===");
            Console.ResetColor();

            int rentalId = validator.ReadInt("Enter Rental ID: ");
            Rental rental = GetRentalById(rentalId);

            if (rental == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Rental not found.");
                Console.ResetColor();
                return;
            }

            if (rental.Status != "Ongoing")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Rental already returned.");
                Console.ResetColor();
                return;
            }

            rental.Status = "Returned";
            Car car = GetCarById(rental.CarId);
            if (car != null)
                car.Status = "Available";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Car returned successfully!");
            Console.ResetColor();
        }

        // ===================== MAINTENANCE =====================
        public void LogMaintenance()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== LOG MAINTENANCE ===");
            Console.ResetColor();

            ShowAllCars();

            int carId = validator.ReadInt("Enter Car ID: ");
            Car car = GetCarById(carId);

            if (car == null || car.Status == "Rented")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Car not found or currently rented.");
                Console.ResetColor();
                return;
            }

            string by = validator.ReadNonEmpty("Performed By: ");
            DateTime completed = validator.ReadDate("Date Completed (YYYY-MM-DD): ");
            string details = validator.ReadNonEmpty("Maintenance Details: ");

            int id = GetNextMaintenanceId();
            MaintenanceRecord record = new MaintenanceRecord(id, carId, by, completed.ToString("yyyy-MM-dd"), details);
            maintenanceRecords.Add(record);

            car.Status = "Maintenance";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Maintenance logged. Car status set to Maintenance.");
            Console.ResetColor();
        }

        public void FinishMaintenance()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=== FINISH MAINTENANCE ===");
            Console.ResetColor();

            int carId = validator.ReadInt("Enter Car ID: ");
            Car car = GetCarById(carId);

            if (car == null || car.Status != "Maintenance")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Car not found or not under maintenance.");
                Console.ResetColor();
                return;
            }

            car.Status = "Available";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Car marked as Available.");
            Console.ResetColor();
        }

        // ===================== DATE CONFLICT =====================
        private bool HasDateConflict(int carId, DateTime newStart, DateTime newEnd)
        {
            foreach (var r in rentals)
            {
                if (r.CarId == carId && r.Status == "Ongoing") // Reserved removed
                {
                    if (DateTime.TryParse(r.StartDate, out DateTime existingStart) &&
                        DateTime.TryParse(r.EndDate, out DateTime existingEnd))
                    {
                        if (newStart <= existingEnd && newEnd >= existingStart)
                            return true;
                    }
                }
            }
            return false;
        }

        // ===================== HELPERS =====================
        private Car GetCarById(int id) => cars.FirstOrDefault(c => c.CarId == id);
        private Customer GetCustomerById(int id) => customers.FirstOrDefault(c => c.CustomerId == id);
        private Rental GetRentalById(int id) => rentals.FirstOrDefault(r => r.RentalId == id);

        private int AddCustomer(string name, string contact)
        {
            int id = GetNextCustomerId();
            customers.Add(new Customer(id, name, contact));
            return id;
        }

        private int GetNextCustomerId() => customers.Count > 0 ? customers.Max(c => c.CustomerId) + 1 : 1;
        private int GetNextRentalId() => rentals.Count > 0 ? rentals.Max(r => r.RentalId) + 1 : 1;
        private int GetNextMaintenanceId() => maintenanceRecords.Count > 0 ? maintenanceRecords.Max(m => m.MaintenanceId) + 1 : 1;

        // ===================== RECEIPT =====================
        private void PrintAndSaveReceipt(Rental rental)
        {
            Car car = GetCarById(rental.CarId);
            Customer cust = GetCustomerById(rental.CustomerId);

            string receipt =
                "===== CAR RENTAL RECEIPT =====\n" +
                "Rental ID: " + rental.RentalId + "\n" +
                "Date Issued: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n" +
                "Customer: " + cust.FullName + "\n" +
                "Contact: " + cust.ContactNumber + "\n\n" +
                "Car: " + car.Brand + " " + car.Model + "\n" +
                "Plate: " + car.PlateNumber + "\n" +
                "Year: " + car.Year + "\n\n" +
                "Start Date: " + rental.StartDate + "\n" +
                "End Date: " + rental.EndDate + "\n" +
                "Status: " + rental.Status + "\n" +
                "==============================\n";

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(receipt);
            Console.ResetColor();

            receiptPrinter.SaveReceipt(receipt);
        }

        // ===================== CSV LOAD / SAVE =====================
        private void LoadCars()
        {
            var lines = fileManager.ReadAllLines(carsFile);
            cars.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                var p = lines[i].Split(',');
                cars.Add(new Car(int.Parse(p[0]), p[1], p[2], p[3], int.Parse(p[4]), p[5]));
            }
        }

        private void LoadCustomers()
        {
            var lines = fileManager.ReadAllLines(customersFile);
            customers.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                var p = lines[i].Split(',');
                customers.Add(new Customer(int.Parse(p[0]), p[1], p[2]));
            }
        }

        private void LoadRentals()
        {
            var lines = fileManager.ReadAllLines(rentalsFile);
            rentals.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                var p = lines[i].Split(',');
                rentals.Add(new Rental(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]), p[3], p[4], p[5]));
            }
        }

        private void LoadMaintenance()
        {
            var lines = fileManager.ReadAllLines(maintenanceFile);
            maintenanceRecords.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                var p = lines[i].Split(',');
                maintenanceRecords.Add(new MaintenanceRecord(int.Parse(p[0]), int.Parse(p[1]), p[2], p[3], p[4]));
            }
        }

        private void LoadAdmins()
        {
            var lines = fileManager.ReadAllLines(adminsFile);
            admins.Clear();
            for (int i = 1; i < lines.Count; i++)
            {
                var p = lines[i].Split(',');
                admins.Add(new Admin(int.Parse(p[0]), p[1], p[2]));
            }
        }

        private void SaveCars()
        {
            var lines = new List<string> { "CarId,PlateNumber,Brand,Model,Year,Status" };
            lines.AddRange(cars.Select(c => c.ToCsv()));
            fileManager.WriteAllLines(carsFile, lines);
        }

        private void SaveCustomers()
        {
            var lines = new List<string> { "CustomerId,FullName,ContactNumber" };
            lines.AddRange(customers.Select(c => c.ToCsv()));
            fileManager.WriteAllLines(customersFile, lines);
        }

        private void SaveRentals()
        {
            var lines = new List<string> { "RentalId,CarId,CustomerId,StartDate,EndDate,Status" };
            lines.AddRange(rentals.Select(r => r.ToCsv()));
            fileManager.WriteAllLines(rentalsFile, lines);
        }

        private void SaveMaintenance()
        {
            var lines = new List<string> { "MaintenanceId,CarId,PerformedBy,DateCompleted,Details" };
            lines.AddRange(maintenanceRecords.Select(m => m.ToCsv()));
            fileManager.WriteAllLines(maintenanceFile, lines);
        }

        private void SaveAdmins()
        {
            var lines = new List<string> { "AdminId,Username,Password" };
            lines.AddRange(admins.Select(a => a.ToCsv()));
            fileManager.WriteAllLines(adminsFile, lines);
        }

        // ===================== DAILY REPORT =====================
        public void DailyReport()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== DAILY REPORT ===");
            Console.ResetColor();

            int totalCars = cars.Count;
            int availableCars = cars.Count(c => c.Status == "Available");
            int rentedCars = cars.Count(c => c.Status == "Rented");
            int maintenanceCars = cars.Count(c => c.Status == "Maintenance");
            int totalRentals = rentals.Count;

            Console.WriteLine($"Total Cars: {totalCars}");
            Console.WriteLine($"Available Cars: {availableCars}");
            Console.WriteLine($"Rented Cars: {rentedCars}");
            Console.WriteLine($"Maintenance Cars: {maintenanceCars}");
            Console.WriteLine($"Total Rentals: {totalRentals}");
        }

        public bool LogoutMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n1. Logout");
            Console.WriteLine("2. Exit Program");
            Console.ResetColor();

            int choice = validator.ReadInt("Choose: ");

            if (choice == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Logged out successfully.\n");
                Console.ResetColor();
                return true;   // go back to login
            }
            else if (choice == 2)
            {
                Shutdown();
                Environment.Exit(0);
            }

            return false;
        }
    }
}
