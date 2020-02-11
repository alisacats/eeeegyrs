using System.Collections.Generic;
namespace eeegyr
{
    public class Step
    {
        public string Text { get; set; }

        public IEnumerable<ButtonChoice> Buttons { get; set; }
        
        
        public Navigation Nav { get; set; }
    }
}