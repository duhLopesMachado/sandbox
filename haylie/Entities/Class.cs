using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seClass
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string skill { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public int alive { get; set; }

        ////action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }


        public int _count { get; set; }
        public int _countme { get; set; }
    }
}