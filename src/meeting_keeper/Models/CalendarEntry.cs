using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace meeting_keeper.Models
{
    public class CalendarEntry
    {

        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public long dateCreated { get; set; }
        [ScaffoldColumn(false)]
        public long dateModified { get; set; }
        public string name { get; set; }

        //TODO date and time

    }
}
