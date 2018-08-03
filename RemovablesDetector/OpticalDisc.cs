using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemovablesDetector
{
    public class OpticalDisc
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Drive {Name}");
            sb.AppendLine($"   Volume Label: {Volume}");

            return sb.ToString();
        }
    }
}
