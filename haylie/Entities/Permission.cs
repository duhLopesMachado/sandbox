using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    public class sePermission
    {
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public int id_media { get; set; }
        public string name { get; set; }
    }
}