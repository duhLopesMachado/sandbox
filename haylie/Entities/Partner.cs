using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable]
    public class sePartner
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string partner { get; set; }
        public string cnpj { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        public sePartner(int p_id)
        {
            this.id = p_id;
            this.alive = 1;
            //this._media = new seMedia(0, "");
        }
    }
}