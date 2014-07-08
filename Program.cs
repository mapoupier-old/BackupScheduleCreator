using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupScheduleCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tapeName = "";

            tapeName = ReadConsole("Please enter tape name: ");

            

            Console.Read();
        }


        static string ReadConsole(string Prompt)
        {
            Console.Write(Prompt);
            return Console.ReadLine();
        }
    }
}
