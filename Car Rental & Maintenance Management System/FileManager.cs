using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental___Maintenance_Management_System
{
    public class FileManager
    {
        public void EnsureFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public List<string> ReadAllLines(string path)
        {
            List<string> lines = new List<string>();

            if (File.Exists(path))
            {
                string[] fileLines = File.ReadAllLines(path);
                for (int i = 0; i < fileLines.Length; i++)
                {
                    lines.Add(fileLines[i]);
                }
            }

            return lines;
        }

        public void WriteAllLines(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines.ToArray());
        }

        public void CreateFileIfMissing(string path, List<string> defaultLines)
        {
            if (!File.Exists(path))
            {
                File.WriteAllLines(path, defaultLines.ToArray());
            }
        }
    }
}
