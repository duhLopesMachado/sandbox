using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable]
    public class seQuote
    {
        public int id { get; set; }

        public DateTime dateins { get; set; }

        public string quote { get; set; }

        public string author { get; set; }

        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

    }
}