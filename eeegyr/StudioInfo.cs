using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace eeegyr
{
    public static class StudioInfo
    {
        public static IEnumerable<string> GetPlace(List<Studio> studios)
        {
           return studios.SelectMany(x => x.Place).Distinct();
        }
        public static IEnumerable<string> GetPrice(List<Studio> studios)
        {
            return studios.Select(x => x.Price).Distinct();
        }
        public static IEnumerable<string> GetStyle(List<Studio> studios)
        {
            return studios.SelectMany(x => x.Style).Distinct();
        }
        public static IEnumerable<string> GetType(List<Studio> studios)
        {
            return studios.SelectMany(x => x.TypeOfShooting).Distinct();
        }
        public static IEnumerable<string> GetName(List<Studio> studios)
        {
            return studios.Select(x => x.Name).Distinct();
        }
    }
}