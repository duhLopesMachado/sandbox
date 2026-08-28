using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seProfile
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_user { get; set; }
        public int id_media { get; set; }
        public int id_type { get; set; }

        public string name { get; set; }
        public DateTime datebirth { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string doc { get; set; }
        public string sex { get; set; }

        public string amount { get; set; }
        public string discount { get; set; }
        public string period { get; set; }
        public DateTime dateinit { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux_helpers
        public string hlp_newkey { get; set; }

        //foreign_keys
        public seMedia _media { get; set; }

        public seProfile()
        {
            this._media = new seMedia(0, "");
        }
    }
}