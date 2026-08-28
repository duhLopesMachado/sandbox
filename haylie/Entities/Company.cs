using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seCompany
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_media { get; set; }
        public string name { get; set; }
        public string cnpj { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string site { get; set; }
        public string social_facebook { get; set; }
        public string social_instagram { get; set; }
        public string social_twitter { get; set; }
        public string social_wpp { get; set; }
        public string social_wpptxt { get; set; }

        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux_helpers
        public bool hlp_update { get; set; } = false;

        //foreign_keys
        //public seMedia _media { get; set; }

        public seCompany(int p_id)
        {
            this.id = p_id;
            this.hlp_update = false;
            //this._media = new seMedia(0, "");
        }
    }
}