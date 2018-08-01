using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemovablesDetector
{
    public enum USBEventType
    {
        ConfigurationChanged = 1,
        Inserted = 2,
        Removed = 3,
        Docked = 4,
        Etc = 5
    }

    public class USBEventArgs
    {
        public USBEventType EventType { get; }
        public USBDisk Disk { get; }

        public USBEventArgs(USBEventType Type, USBDisk Disk)
        {
            this.EventType = Type;
            this.Disk = Disk;
        }
    }
}
