using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDetector
{
    public class USBDisk
    {
        /// <summary>
        /// Volume name of the drive (usually the description set for the drive during format)
        /// </summary>
        public string Volume { get; set; }

        /// <summary>
        /// Name of the drive generated for windows (usually the drive letter)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DirectoryInfo object for the root directory to directly interface with IO
        /// </summary>
        public DirectoryInfo EntryPoint { get; set; }

        /// <summary>
        /// Available drive space in GBytes
        /// </summary>
        public Int64? AvailableSpace { get; set; }

        /// <summary>
        /// Total drive space in GBytes
        /// </summary>
        public Int64? TotalSpace { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Drive {Name}");
            sb.AppendLine($"   Volume Label: {Volume}");
            sb.AppendLine($"   Space Free: {AvailableSpace} GBytes");
            sb.AppendLine($"   Space Total: {TotalSpace} GBytes");

            return sb.ToString();
        }
    }
}
