using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BagageSortering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0, 0);

            object writerLock = new object();

            TextBox checkInBox = new TextBox("Check-In", 60, 10, 3, 2, writerLock);

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
