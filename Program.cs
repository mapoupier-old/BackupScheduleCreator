using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BackupScheduleCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tapeName = "";
            var tapeCount = "";
            var rententionPeriod = "";

            tapeName = ReadConsole("Please enter tape name: ");
            tapeCount = ReadConsole("How many tapes: ");

            var tapeList = InitializeTapeList(Convert.ToInt16(tapeCount) + 1, tapeName);

            var sw = new StreamWriter(@"C:\temp\backup.csv");
            sw.AutoFlush = true;
            var currentDate = new DateTime(2014, 7, 5);

            //for(int i = 1; i < Convert.ToInt16(tapeCount) + 1; i++)
            //{
            //    var monthly = tape.Value.Day <= 7 ? "M" : "W";
            //    var newExpiry = tape.Value.AddDays(28);
            //    currentDate = tape.Value;

            //    if (monthly == "M")
            //        newExpiry = new DateTime(tape.Value.AddMonths(6).Year, tape.Value.AddMonths(6).Month, 1);

            //    sw.WriteLine(string.Format("{0},{1},{2},{3}", tape.Value.ToString("MM/dd/yyyy"), tape.Key, monthly, newExpiry.ToString("MM/dd/yyyy")));

            //    tapeList[tape.Key] = newExpiry;
            //}

            for (int v = 0; v < 900; v++)
            {
                var tape = tapeList.OrderBy(x => x.Value).Where(x => x.Value <= currentDate).Take(1).First();

                var monthly = currentDate.Day <= 7 ? "M" : "W";
                var newExpiry = currentDate.AddDays(28);


                if (monthly == "M")
                    newExpiry = GetFirstSarturday(tape.Value, 6);

                sw.WriteLine(string.Format("{0},{1},{2},{3}", currentDate.ToString("MM/dd/yyyy"), tape.Key, monthly, newExpiry.ToString("MM/dd/yyyy")));

                tapeList[tape.Key] = newExpiry;
                Console.WriteLine(string.Format("{0} -> {1}", currentDate.ToShortDateString(), newExpiry.ToShortDateString()));

                currentDate = currentDate.AddDays(7);

            }


            Console.Read();
        }

        private static DateTime GetFirstSarturday(DateTime dateTime, int p)
        {
            var d = dateTime.AddMonths(p);

            int i = DayOfWeek.Saturday - d.DayOfWeek;

            d = d.AddDays(i);

            return d;
        }


        static string ReadConsole(string Prompt)
        {
            Console.Write(Prompt);
            return Console.ReadLine();
        }

        static Dictionary<string, DateTime> InitializeTapeList(int TapeCount, string TapeName)
        {
            Dictionary<string, DateTime> tapeList = new Dictionary<string, DateTime>();

            var initDate = new DateTime(2014, 7, 5);

            for (int i = 1; i < TapeCount; i++)
            {
                tapeList.Add(string.Format("{0}-{1}", TapeName, i.ToString("D4")), initDate);
                initDate = initDate.AddDays(7);
            }

            return tapeList;
        }
    }
}
