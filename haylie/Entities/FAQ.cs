using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seFAQ
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_media { get; set; }
        public string question{ get; set; }
        public string answer { get; set; }
        
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux_helpers
        //public bool hlp_update { get; set; } = false;

        //foreign_keys
        //public seMedia _media { get; set; }

        public seFAQ(int p_id)
        {
            this.id = p_id;
            //this.hlp_update = false;
            //this._media = new seMedia(0, "");
        }
    }
}