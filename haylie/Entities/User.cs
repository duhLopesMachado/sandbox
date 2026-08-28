using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seUser
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_permission { get; set; }
        public int id_company { get; set; }
        public string mail { get; set; }
        public string keycode { get; set; }
        public string token { get; set; }
        public string template { get; set; }
        public int alive { get; set; }

        public int id_avatar { get; set; }
        public int id_plan { get; set; }

        public string _avatar { get; set; }
        public string _name { get; set; }
        public string _phone { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //foreign_keys
        public seCompany _company { get; set; }
        public sePlan _plan { get; set; }
        public seProfile _profile { get; set; }
        public sePermission _permission { get; set; }

        //extra . helpers
        public string hlp_ip { get; set; }

        public seUser(string p_mail, string p_key, string p_ip = "")
        {
            this.mail = p_mail;
            this.keycode = p_key;
            this.hlp_ip = p_ip;
            
            //this._company = new seCompany(0);
            this._profile = new seProfile();
            this._permission = new sePermission();
            this._plan = new sePlan();
        }
    }
}