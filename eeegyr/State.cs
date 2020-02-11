using System.Collections.Generic;

namespace eeegyr
{   
    public class State
    {
        public List<Step> Steps { get; set; }

        public Step CurrentStep { get; set; }
        
    }
}