using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seLead
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //auxxx 
        public string _name { get; set; }

        public seLead(string p_name, string p_phone, string p_mail)
        {
            this.id = 0;
            this.alive = 1;
            this.log_date = new DateTime();
            this.log_date = DateTime.Now;

            this.name = p_name;
            this.mail = p_mail;
            this.phone = p_phone.Trim().Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "");
            
        }
    }
}