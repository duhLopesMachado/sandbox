using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seAgenda
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string padawan { get; set; }
        public string skill { get; set; }
        public DateTime dateto { get; set; }
        public string dateto_short { get; set; }
        public string dateto_label { get; set; }
        public string datetotime { get; set; }
        public int status { get; set; }
        public int device { get; set; }

        ////action_log
        public int log_user { get; set; }
        //public DateTime log_date { get; set; }


        public bool _member { get; set; } = false;
        public int _count { get; set; } = 0;

        public seAgenda(string p_padawan = "", string p_skill = "", string p_device = "0")
        {
            this.id = 0;
            this.padawan = p_padawan;
            this.skill = p_skill;
            this.device = Convert.ToInt32(p_device);
            this.status = 1;
        }
    }
    [Serializable()]
    public class seAgendaDay
    {
        public string skill { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public string dayofweek { get; set; }
        public string dateshort { get; set; }
        public int requests { get; set; }
        public int confirmed { get; set; }
    }
}