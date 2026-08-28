using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Entities
{
    [Serializable()]
    public class seMedia
    {
        //entity_mirror 
        public int id { get; set; }
        public DateTime dateins { get; set; }
        public string name { get; set; }

        public int _file_alive { get; set; }

        public seMedia(int p_id, string p_name)
        {
            this.id = p_id;
            this.name = p_name.Length > 0 ? p_name : "~/content/img/photo.png";
            this.dateins = new DateTime();
            this._file_alive = 1;
        }
    }
}