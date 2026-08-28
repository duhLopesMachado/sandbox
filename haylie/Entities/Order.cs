using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    public class seOrder
    {
        //entity mirror
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_partner { get; set; }
        public int alive { get; set; }

        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

        //foreign_keys
        public sePartner _partner { get; set; }
        public seUser _client { get; set; }

        //virtual
        public List<seOrderItem> list_items = new List<seOrderItem>();

        public List<seProduct> list_products = new List<seProduct>();

        public seOrder() {
            this._partner = new sePartner(0);
            this._client = new seUser("","");
        }
    }
}