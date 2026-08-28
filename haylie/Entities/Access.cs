using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    public class seAccess
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }
    }
}