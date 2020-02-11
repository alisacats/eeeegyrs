using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eeegyr.Xls;
using NPOI.OpenXmlFormats.Spreadsheet;

namespace eeegyr
{
    class Program
    {
        static async void res()
        {
            //  await HandlerMessage.Test();
        }

        static void Main(string[] args)
        {
            var table = XlsTable.LoadFromFile(@"C:\dev\eeegyr-master\Studio_Bot(2).xlsx");
            var rows = table.GetRows().ToArray();
            var t = Enumerable.Range(0, table.ColumnsCount).Select(x => rows[4][x]).ToArray();
            List<Studio> allstudios = new List<Studio>();
            for (int i = 1; i < table.RowsCount; i++)
            {
                var test = Enumerable.Range(0, table.ColumnsCount).Select(x => rows[i][x]).ToArray();
                var studio = new Studio()
                {
                    Name = test[0],
                    Place = test[5].Split(','),
                    Price = test[2],
                    Style = test[3].Split(','),
                    TypeOfShooting = test[4].Split(','),
                    Url = test[1]
                };
                allstudios.Add(studio);


                HandlerMessage.SendMessage();

                //  HandlerMessage.Test();
                Console.ReadLine();

            }
        }
    }
}