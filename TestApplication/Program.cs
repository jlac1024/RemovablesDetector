using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemovablesDetector;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var usbWatcher = new EventWatcher();

            usbWatcher.DeviceInserted += (s, ea) =>
            {
                Console.WriteLine($"{ea.Disk.Name} was {ea.EventType}");
                Console.WriteLine($"{ea.Disk}");
            };
            usbWatcher.DeviceRemoved += (s, ea) =>
            {
                Console.WriteLine($"{ea.Disk.Name} was {ea.EventType}");
            };

            usbWatcher.Start();

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
        }
    }
}
