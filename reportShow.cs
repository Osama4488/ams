using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Controllers
{
    public class reportShow
    {
        public string Day_no { get; set; }
        public string Total_Hours { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }

        public Nullable<System.DateTime> endTime { get; set; }
        
        public Nullable<System.DateTime> startTime { get; set; }


    }
}