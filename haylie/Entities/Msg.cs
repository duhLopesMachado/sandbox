using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable]
    public class seMsg
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_user { get; set; }
        public int id_media { get; set; }
        public string msg { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux
        public string _name { get; set; }
        public string _avatar { get; set; }


        public seMsg(int p_from, int p_to, string p_msg)
        {
            this.id = 0;
            this.id_user = p_to;
            this.log_user = p_from;
            this.msg = p_msg;
            this.alive = 1;
        }
    }
}