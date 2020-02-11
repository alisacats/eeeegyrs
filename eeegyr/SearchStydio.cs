using System.Collections.Generic;
using System.Linq;

namespace eeegyr
{
    public static class SearchStudio
    {
        public static bool Helper(IEnumerable<string>allStudios, string studiosForUser)
        {
            foreach (var studio in allStudios)
            {
                if (studio == studiosForUser)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Helper(IEnumerable<string>allStudios, IEnumerable<string>studiosForUser)
        {
            foreach (var studio in allStudios)
            {
                if (studiosForUser.Contains(studio))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static List<Studio> SearchStudioByPlace(List<Studio> studios, IEnumerable<string> placeForUser)
        {
            return studios.Where(x => Helper(placeForUser, x.Place)).ToList();
        }
        public static List<Studio> SearchStudiosByPrice(List<Studio> studios, IEnumerable<string> priceForUser)
        {
            return studios.Where(x => Helper(priceForUser, x.Price)).ToList();
        }
        public static List<Studio> SearchStudioByStyle(List<Studio> studios, IEnumerable<string> styleForUser)
        {
            return studios.Where(x=>Helper(styleForUser,x.Style)).ToList();
        }
        public static List<Studio> SearchStudioByType(List<Studio> studios, IEnumerable<string> typeShootingForUser)
        {
            return studios.Where(x=>Helper(typeShootingForUser,x.TypeOfShooting)).ToList();
        }

    /*    public static List<Stydio> FinalSearch(List<Stydio>stydios,IEnumerable<string>daet)
        {
           var e = SearchPlace(stydios,daet);
           SearchPlace(e, daet);
           return new List<Stydio>();
        }*/
    }
}