using System;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CAAssembler
{
    class Program
    {
        const string INPUTPATH = "input.txt";
        const string OUTPUTPATH = "output.txt";

        static void Main(string[] args)
        {
            ulong parseCnt = 0;
            ulong parseFailCnt = 0;

            if (!File.Exists(INPUTPATH))
            {
                Console.WriteLine("Cannot find input file : " + INPUTPATH);
                Console.ReadKey();
                return;
            }

            try
            {
                using (StreamWriter wr = new StreamWriter(OUTPUTPATH))
                using (StreamReader rd = new StreamReader(INPUTPATH))
                {
                    string? line;

                    while ((line = rd.ReadLine()) != null)
                    {
                        parseCnt++;
                        Console.Write("Assembling {0} lines...\r", parseCnt);


                        var assembled = OPCODE.Assemble(line);

                        if (assembled is not null)
                        {
                            wr.WriteLine(assembled.ToString());
                        }
                        else
                        {
                            parseFailCnt++;
                            wr.WriteLine(new string('0', 32) + " Failed");
                        }

                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            finally {; }

            Console.WriteLine("Assembling Done!", parseCnt);
            Console.WriteLine("ParseCnt : {0}", parseCnt);
            Console.WriteLine("ParseFailCnt : {0}", parseFailCnt);


            Console.WriteLine("Press any key to Exit..");
            Console.ReadKey();
        }
    }

}