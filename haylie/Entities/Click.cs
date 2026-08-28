using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{

    [Serializable()]
    public class seClick
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string tag { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        public seClick(int p_id, string p_tag)
        {
            this.id = p_id;
            this.tag = "click";
            this.alive = 2;
        }

    }
}