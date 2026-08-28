using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seProduct
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_type { get; set; }
        public int id_tag { get; set; }
        public string name { get; set; }
        public string unittype { get; set; }
        public string unitqtd { get; set; }
        public string stock { get; set; }
        public string stockmin { get; set; }
        public string pricein { get; set; }
        public string priceout { get; set; }

        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //aux_helpers
        public int hlp_company { get; set; }

        public seProduct(int p_id, string p_name)
        {
            this.id = p_id;
            this.name = p_name;
        }
    }
}