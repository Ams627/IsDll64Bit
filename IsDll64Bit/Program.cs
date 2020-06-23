using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace IsDll64Bit
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (!args.Any())
                {
                    throw new Exception($"You must supply one or more filenames.");
                }
                foreach (var filename in args)
                {
                    var buffer = new byte[2048];
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        stream.Read(buffer, 0, 2048);
                        var offset = BitConverter.ToInt32(buffer, 0x3c);
                        var machineType = BitConverter.ToUInt16(buffer, offset + 4);
                        Console.WriteLine($"0x{machineType:X}");
                    }
                }
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine($"{progname} Error: {ex.Message}");
            }

        }
    }
}
