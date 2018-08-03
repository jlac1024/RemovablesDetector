using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemovablesDetector
{
    public enum OpticalEventType
    {
        Inserted,
        Removed,
    }

    public class OpticalEventArgs
    {
        public OpticalEventType EventType { get; }
        public OpticalDisc Disc { get; }
    }
}
