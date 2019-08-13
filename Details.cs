using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMS.Controllers
{
    public class Details
    {
        internal string dailytimestatus;

        public int Id { get; set; }
        public string Name { get; set; }

       

        public Nullable<System.DateTime> start_time { get; set; }
        public Nullable<System.DateTime> end_time { get; set; }

        public Nullable<int> Contact { get; set; }



        public string Day { get; set; }

        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }

        public string Status { get; set; }

        public string Month { get; set; }

        //public Nullable<System.DateTime> breakendTime { get; set; }
        //public Nullable<System.DateTime> breakstartTime { get; set; }

        public int check { get; set; }


        public int breakcheck { get; set; }

    }
}