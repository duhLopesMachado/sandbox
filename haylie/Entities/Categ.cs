using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seCateg
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_sector { get; set; }
        public string name { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        public seCateg(int p_id, string p_name) {
            this.id = p_id;
            this.id_sector = 1;
            this.name = p_name;
            this.alive = 1;
        }

    }
}