using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seGameType
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        //public int id_user { get; set; }
        public string title { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux
        public string _type { get; set; }

        public seGameType(int p_id, string p_title, int p_user)
        {
            this.id = p_id;
            this.title = p_title;
            this.log_user = p_user;
            this.alive = 1;
        }
    }

    [Serializable()]
    public class seGameLog
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        //public int id_user { get; set; }
        public int id_type { get; set; }
        public int amount { get; set; }
        public string title { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux
        public string _type { get; set; }

        public seGameLog(int p_type, string p_title, int p_user)
        {
            this.id = 0;
            this.id_type = p_type;
            this.title = p_title;
            this.log_user = p_user;
            this.alive = 1;
        }
    }
}