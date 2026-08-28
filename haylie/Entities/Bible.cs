using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable]
    public class seBible
    {
        public int id { get; set; }

        public DateTime dateins { get; set; }

        public string txt { get; set; }

        public string book { get; set; }

        public int chapter { get; set; }

        public int verse { get; set; }

        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

    }
}