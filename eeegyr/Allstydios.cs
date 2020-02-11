
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eeegyr.Xls;
using LiteDB;

namespace eeegyr
{
    public class Allstudios
    {
        public List<Studio> FullStudios()
        {
            var table = XlsTable.LoadFromFile(@"C:\dev\eeegyr-master\Studio_Bot.xlsx");
            var rows = table.GetRows().ToArray();
            var t = Enumerable.Range(0, table.ColumnsCount).Select(x => rows[4][x]).ToArray();
            List<Studio> allstudios = new List<Studio>();
            for (int i = 1; i < table.RowsCount; i++ )
            {
                var test  = Enumerable.Range(0, table.ColumnsCount).Select(x => rows[i][x]).ToArray();
                var studio = new Studio()
                {
                    Name = test[0],
                    Place = test[5].Split(',',StringSplitOptions.RemoveEmptyEntries),
                    Price = test[2],
                    Style = test[3].Split(',',StringSplitOptions.RemoveEmptyEntries),
                    TypeOfShooting = test[4].Split(',',StringSplitOptions.RemoveEmptyEntries),
                    Url = test[1]                        
                };
                allstudios.Add(studio);
            }

            return allstudios;
        }
        public void PutStudiosAtDB(List<Studio> studios)
        {
            using (var db = new LiteDatabase(@"MyStudios.db"))
            {
                var allStudio = db.GetCollection<ForDB>("studios");
                foreach (var studio in studios)
                {
                    var studioForDB = new ForDB()
                    {
                        Studio = studio
                    };
                    allStudio.Insert(studioForDB);
                }
            }
        }

        public string [] SplitToLine(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            { 
                String text = sr.ReadToEnd();
                return text.Split("\r\n");
            }
        }
        public bool IsStringNonEmpty(string s)
        {
           return string.IsNullOrWhiteSpace(s);
        }
        public Studio ParseLineOnStudio(string line)
        {
            var lines = line.Split(' ');
            string[] styleStudio = new []{lines[3]} ;
            string[] typeStudio = new[] {lines[1]};
            string[] placeStudio = new[] {lines[2]};
             
            return new Studio()
            {
                Name = lines[0],
                TypeOfShooting = styleStudio,
                Place = placeStudio,
                Style = typeStudio,
                Price = lines[4]
            };
        }

        public List<Studio> ParseStudios(string allStudios)
        {  
            var studios = new List<Studio>();
            var lines = SplitToLine(allStudios);
            foreach (var line in lines)
            {
                studios.Add(ParseLineOnStudio(line));
            }
            return studios;
        }
    }
}