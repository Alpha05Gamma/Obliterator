using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileObcerver
{
    static class Obcerver
    {
        static Process process = new Process();
        public static List<int> Obcerve ()
        {
            Console.SetCursorPosition(0, 2);
            List<int> list = new List<int>();

            foreach(Process process in Process.GetProcesses())
            {
                
                Console.WriteLine($"  ID: {process.Id}  Name: {process.ProcessName}");
                list.Add(process.Id);
               
            }
            Console.SetCursorPosition(0, 0);
            return list;
        }
        public static void Run(string name)
        {
            try
            {
                if (File.Exists(name)) //Если файл, то запуск через PowerShell
                {
                    Process.Start(new ProcessStartInfo { FileName = name, UseShellExecute = true });
                }
            }
            catch (Exception exception)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(exception.Message);
            }
        }
        public static void ObcerveInfo(int indexId)
        {
            

            foreach (Process process in Process.GetProcesses())
            {
                if (process.Id == indexId)
                {
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 6);
                    Console.Write($"  Physical memory usage     : {process.WorkingSet64}");
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 7);
                    Console.Write($"  Base priority             : {process.BasePriority}");
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 8);
                    Console.Write($"  Paged system memory size  : {process.PagedSystemMemorySize64} Bytes");
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 9);
                    Console.Write($"  Paged memory size         : {process.PagedMemorySize64} Bytes");
                }
                

            }
            Console.SetCursorPosition(0, 0);
        }

        public static void Obliterator(int indexId)
        {
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.Id == indexId)
                    {
                       process.Kill();
                    }


                }
            }
            catch (Exception exception)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(exception.Message);
            }
        }
    }
}
