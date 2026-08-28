using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    public class seOrderItem
    {
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_order { get; set; }
        public int id_product { get; set; }
        public decimal qtd { get; set; }
        public int alive { get; set; }
        //action_log
        public int log_user { get; set; }
        public DateTime log_date { get; set; }

    }
}