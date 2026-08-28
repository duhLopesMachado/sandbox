using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seAlert
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_user { get; set; }
        public int id_type { get; set; }
        public string title { get; set; }
        public string resume { get; set; }
        public string msg { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux
        public string _name { get; set; }

        public seAlert(int p_userTo, string p_title, string p_resume, string p_msg)
        {
            this.id = 0;
            this.id_user = p_userTo;
            this.title = p_title;
            this.resume = p_resume;
            this.msg = p_msg;
            this.alive = 1;
        }
    }
}