using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAAssembler
{
    class OutFile
    {

        //OUTPUT Configuration
        const UInt32 BINARY_SIZE = 256;
        const UInt32 CODE_START = 0;
        const UInt32 DATA_START = 128;

        private readonly string _outpath;

        private UInt32 _code_count = 0;

        List<UInt32> _output;

        public OutFile(string datapath)
        {
            _outpath = datapath;
            _output = Enumerable.Repeat((UInt32)0, (int)BINARY_SIZE).ToList();
        }

        public bool AppendCode(UInt32 code)
        {
            UInt32 codePtr = CODE_START + _code_count;

            if ((codePtr >= BINARY_SIZE) || (codePtr < 0)) return false;

            _output[(int)codePtr] = code;

            _code_count++;

            Console.WriteLine("[{0:X6}][CODE] : {0:X8}", codePtr * 4, code);
            return true;
        }

        public bool AppendCode(string binaryStr)
        {
            UInt32 codePtr = CODE_START + _code_count;

            if ((codePtr >= BINARY_SIZE) || (codePtr < 0)) return false;
            if (!binaryStr.All(c => c == '0' || c == '1') || binaryStr.Length != 32) return false;

            UInt32 code = Convert.ToUInt32(binaryStr, 2);
            _output[(int)codePtr] = code;

            _code_count++;

            Console.WriteLine("[{0:X6}][CODE] : {0:X8}", codePtr * 4, code);
            return true;
        }

        public bool AppendData(UInt32 ptr, UInt32 data)
        {
            UInt32 dataPtr = DATA_START + ptr;

            if ((dataPtr >= BINARY_SIZE) || (dataPtr < DATA_START)) return false;

            _output[(int)dataPtr] = data;

            Console.WriteLine("[{0:X6}][DATA] : {0:X8}", dataPtr * 4);

            return true;
        }

        public bool WriteFile()
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(_outpath))
                {
                    //순서 보장됨
                    foreach (var value in _output)
                    {
                        string binaryString = Convert.ToString(value, 2).PadLeft(32, '0');
                        wr.WriteLine(binaryString);
                    }
                }

                return true;
            }
            catch (Exception) { return false; }
        }

    }
}
