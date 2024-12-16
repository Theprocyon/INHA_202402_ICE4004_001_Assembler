using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAAssembler
{
    class DataParser
    {
        private DataParser() { }

        private static readonly string DATA_IDENTIFIER = "DATA";

        public static KeyValuePair<UInt32, UInt32>? ParseHex(string line)
        {
            if (string.IsNullOrEmpty(line)) return null;
            UInt32 dataptr = 0;
            UInt32 data = 0;
            string[] parsed = line.Split(',');

            if (parsed.Length != 3) return null;

            string id = parsed[0].Trim();

            if (id != DataParser.DATA_IDENTIFIER) return null;

            try
            {
                string dataPtrStr = parsed[1].Trim();

                dataptr = UInt32.Parse(dataPtrStr);

                string datastr = parsed[2].Trim();

                data = UInt32.Parse(datastr, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception) { return null; }

            return new KeyValuePair<UInt32, UInt32>(dataptr, data);
        }

    }
}
