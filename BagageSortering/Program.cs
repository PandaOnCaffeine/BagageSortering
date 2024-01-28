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

            Thread.Sleep(2000);


            object writerLock = new object();
            object keyLock = new object();

            int y = 2;

            int bagageKey = 1;

            int gateAmount = 5;
            int checkInsAmount = 5;

            Buffer[] gateBuffers = new Buffer[gateAmount];
            Buffer sortingBuffer = new Buffer("Sorting Machine", 15);

            Gate[] gates = new Gate[gateAmount];
            Check_In[] checkIns = new Check_In[checkInsAmount];
            SortingMachine sorting = new SortingMachine(sortingBuffer, gateBuffers, new TextBox("Sorting Machine", 60, 15, 76, 10, writerLock));

            Thread[] gateThreads = new Thread[gateAmount];
            Thread[] checkInsThreads = new Thread[checkInsAmount];
            Thread sortingThread = new Thread(new ThreadStart(sorting.Run));





            for (int i = 1; i < gates.Length + 1; i++)
            {
                gateBuffers[i - 1] = new Buffer($"Gate {i}", 10);
                gates[i - 1] = new Gate(gateBuffers[i - 1], new TextBox($"Gate {i}", 60, 10, 150, y, writerLock));
                gateThreads[i - 1] = new Thread(new ThreadStart(gates[i - 1].open));
                gateThreads[i - 1].Start();
                y += 14;
            }

            y = 2;

            for (int i = 1; i < checkIns.Length + 1; i++)
            {
                checkIns[i - 1] = new Check_In(new Buffer($"CheckIn{i}", 10), new TextBox($"Check In {i}", 60, 10, 3, y, writerLock),sortingBuffer, gateAmount,bagageKey,keyLock);
                checkInsThreads[i - 1] = new Thread(new ThreadStart(checkIns[i - 1].Run));
                checkInsThreads[i - 1].Start();
                y += 14;
            }
            sortingThread.Start();


            Console.ReadLine();
        }
    }
}
