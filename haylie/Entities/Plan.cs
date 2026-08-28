using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class sePlan
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string name { get; set; }
        public string skill { get; set; }
        public string type { get; set; }
        public string txt { get; set; }
        public string price { get; set; }
        public int alive { get; set; }

        public DateTime limit_date { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        public int _count { get; set; }

        public sePlan(string p_name="", string p_txt="", string p_price="")
        {
            this.id = 0;
            this.alive = 1;
            this.log_date = new DateTime();
            this.log_date = DateTime.Now;

            this.name = p_name;
            this.txt = p_txt;
            this.price = p_price;

        }
    }
}