using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace eeegyr
{
    public class ParceMessage
    {
        public string reslut="";
        public string [][] creats = new string[10][];
        public int index = 0;
        public string GetStude(State state)
        {
            var e = state.Steps;
            foreach (var step in e)
            {     
                creats[index] =  step.Buttons.Where(x => x.Flag).Select(x => x.Text).ToArray();
                index++;
            }
            var res = new Allstudios();
            var maintest = res.FullStudios();
            var test1 = SearchStudio.SearchStudiosByPrice(maintest,creats[0]);
            var test2 = SearchStudio.SearchStudioByPlace(test1,creats[1]);
            var test3 = SearchStudio.SearchStudioByStyle(test2,creats[2]);
            var test4 = SearchStudio.SearchStudioByType(test3,creats[3]);
            foreach (var studio in test4)
            {
                reslut = reslut + studio.Name + "\n" + studio.Url;
            }
            return reslut;
        }
    }
}