using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDetector
{
    public static class DataHelpers
    {
        public static Int64 BytesToKilobytes(this Int64 Bytes)
        {
            return Bytes / 1024;
        }

        public static Int64 BytesToMegabytes(this Int64 Bytes)
        {
            return (Bytes / 1024) / 1024;
        }

        public static Int64 BytesToGigabytes(this Int64 Bytes)
        {
            return ((Bytes / 1024) / 1024) / 1024;
        }
    }
}
