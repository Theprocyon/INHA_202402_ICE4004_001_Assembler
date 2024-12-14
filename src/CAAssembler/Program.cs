using System;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;

class Program
{

    const string INPUTPATH = "input.csv";
    const string BINPATH = "output.txt";

    public class OPCODE
    {
        enum InstType
        {
            I, R, G, J
        }
        private readonly string _name;

        private readonly byte _opcode;
        private readonly  InstType _insttype;

        private static readonly List<OPCODE> List = new();

        private OPCODE(string _name, byte _opcode, InstType _insttype)
        {
            this._name = _name; 
            this._insttype = _insttype; 
            this._opcode = _opcode;

            List.Add(this);
        }

        public static readonly OPCODE ADD = new("ADD", 0b_00_0000, InstType.R);
        public static readonly OPCODE SUB = new("SUB", 0b_00_0001, InstType.R);
        public static readonly OPCODE CMP = new("CMP", 0b_00_0010, InstType.R);
        public static readonly OPCODE MUL = new("MUL", 0b_00_0011, InstType.R);
        public static readonly OPCODE BN = new("BN", 0b_01_0000, InstType.J);
        public static readonly OPCODE BEQ = new("BEQ", 0b_01_0001, InstType.J);
        public static readonly OPCODE ADDI = new("ADDI", 0b_01_1000, InstType.I);
        public static readonly OPCODE SUBI = new("SUBI", 0b_01_1001, InstType.I);
        public static readonly OPCODE LOAD = new("LOAD", 0b_01_1010, InstType.I);
        public static readonly OPCODE STORE = new("STORE", 0b_01_1011, InstType.I);
        public static readonly OPCODE MOVS = new("MOVS", 0b_01_1100, InstType.I);
        public static readonly OPCODE LSL = new("LSL", 0b_01_1101, InstType.I);
        public static readonly OPCODE RSL = new("RSL", 0b_01_1110, InstType.I);
        public static readonly OPCODE MULI = new("MULI", 0b_01_1111, InstType.I);
        public static readonly OPCODE GADD9B = new("GADD9B", 0b_10_0000, InstType.G);
        public static readonly OPCODE GSUB9B = new("GSUB9B", 0b_10_0001, InstType.G);
        public static readonly OPCODE GSTORE9B = new("GSTORE9B", 0b_10_0010, InstType.G);
        public static readonly OPCODE GLOAD9B = new("GLOAD9B", 0b_10_0011, InstType.G);
        public static readonly OPCODE GAVG9B = new("GAVG9B", 0b_10_0100, InstType.G);

        public static UInt32? Assemble(string line)
        {
            if (line == null) return null;
            StringBuilder sb = new StringBuilder();

            //for test 241214
            OPCODE? opcode = null;
            InstType? instType = null;

            string[] parsed = line.Split(',');

            if(parsed.Length < 2) return null;

            string opcodeName = parsed[0].Trim();

            //Find OPCODE
            opcode = ParseOPCODE(opcodeName);

            if(opcode == null) return null;

            sb.Append(Convert.ToString(opcode._opcode, 2).PadLeft(6, '0'));

            Console.WriteLine("OPCODE Parse Done : " + sb.ToString());

            switch (opcode._insttype)
            {
                case InstType.I:
                    {
                        if(parsed.Length < 5) return null;
                        byte?   rs1 = ParseReg(parsed[1]);
                        byte?   rs2 = ParseReg(parsed[2]);
                        UInt16? imm = ParseImm(parsed[3]);

                        if(rs1 == null) return null;
                        if(rs2 == null) return null;
                        if(imm == null) return null;

                        sb.Append(Convert.ToString((byte)rs1,   2).PadLeft(5,     '0'));
                        sb.Append(Convert.ToString((byte)rs2,   2).PadLeft(5,     '0'));
                        sb.Append(Convert.ToString((UInt16)imm, 2).PadLeft(16,    '0'));
                    }
                    break;
                case InstType.R:
                    break;
                case InstType.G:
                    break;
                case InstType.J:
                    break;
                default:
                    break;
            }


            Console.WriteLine("------");

            return 0;
        }

        private static OPCODE? ParseOPCODE(string _str)
        {
            string opcode = _str.Trim();
            return OPCODE.List.Find((e) =>
            {
                if (e._name == _str) return true;
                else return false;
            });
        }
        private static byte? ParseReg(string _reg)
        {
            if (byte.TryParse(_reg.Trim(), out byte rbyte) && rbyte <= 31) return rbyte;
            else return null;
        }
        private static UInt16? ParseImm(string _imm)
        {
            if (UInt16.TryParse(_imm.Trim(), out UInt16 rimm)) return rimm;
            else return null;
        }
    }

    static void Main(string[] args)
    {
        if (!File.Exists(INPUTPATH))
        {
            Console.WriteLine("Cannot find input file");
            return;
        }

        try
        {
            using (StreamReader rd = new StreamReader(INPUTPATH))
            {
                string? line;

                while ((line = rd.ReadLine()) != null)
                {




                }



            }
        }
        catch (Exception e) { Console.WriteLine(e.StackTrace); }
        finally {; }


    }
}