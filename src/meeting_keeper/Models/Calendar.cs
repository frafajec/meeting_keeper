using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace meeting_keeper.Models
{

    //EACH user has ONLY ONE, 
    public class Calendar
    {

        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public long dateCreated { get; set; }
        [ScaffoldColumn(false)]
        public long dateModified { get; set; }
        public string name { get; set; }


        public bool showSaturday { get; set; }
        public bool showSunday { get; set; }
        public int timeFrom { get; set; }
        public int timeTo { get; set; }
    }
}
