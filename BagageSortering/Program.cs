using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class Program
    {
        // Import the necessary functions from the Windows API
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private const int SW_MAXIMIZE = 3;
        static void Main(string[] args)
        {
            //maximize console window
            IntPtr consoleHandle = GetConsoleWindow();

            if (consoleHandle != IntPtr.Zero)
            {
                // Maximize the console window
                ShowWindow(consoleHandle, SW_MAXIMIZE);
            }
            Thread.Sleep(1000);






            object writerLock = new object();

            TextBox checkInBox = new TextBox("Check-In", 60, 10, 3, 2, writerLock);
            TextBox sortingBox = new TextBox("Check-In", 60, 10, 3, 2, writerLock);


            Buffer checkIn1Buffer = new Buffer("Nr 1", 10);

            Check_In CheckIn1 = new Check_In(checkIn1Buffer, checkInBox);

            Thread CheckIn1Thread = new Thread(new ThreadStart(CheckIn1.Run));

            CheckIn1Thread.Start();

            Console.ReadKey();

            CheckIn1.Stop();

            CheckIn1Thread.Join();

            Console.ReadLine();
        }
    }
}
