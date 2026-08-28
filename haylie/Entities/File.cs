using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seFile
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_user { get; set; }
        public int id_type { get; set; }
        public int id_media { get; set; }
        public string media_category { get; set; }
        public string media_name { get; set; }
        public string name { get; set; }
        public string resume { get; set; }
        public int site { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux_helpers
        public string hlp_ip { get; set; }
        public int hlp_company { get; set; }

        //foreign_keys
        public seMedia _media { get; set; }

        public seFile()
        {
            this._media = new seMedia(0, "");
        }
    }
}