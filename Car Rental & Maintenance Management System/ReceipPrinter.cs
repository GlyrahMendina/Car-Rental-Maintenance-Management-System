using System;
using System.IO;


namespace Car_Rental___Maintenance_Management_System
{
    public class ReceiptPrinter
    {
        private FileManager fileManager;

        public ReceiptPrinter()
        {
            fileManager = new FileManager();
        }

        public void SaveReceipt(string content)
        {
            fileManager.EnsureFolder("C:\\Users\\Glyrah\\Downloads\\Data\\Receipts.txt");

            string fileName = "Receipts/receipt_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            File.WriteAllText(fileName, content);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Receipt saved to: " + fileName);
            Console.ResetColor();
        }
    }
}
