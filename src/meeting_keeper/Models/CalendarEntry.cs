using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meeting_keeper.Models
{
    public class CalendarEntry
    {

        [Key]
        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public long dateCreated { get; set; }
        [ScaffoldColumn(false)]
        public long dateModified { get; set; }
        public string name { get; set; }

        //TODO date and time
        [ForeignKey("Calendar")]
        [ScaffoldColumn(false)]
        public int calendarID { get; set; }
        public string title { get; set; }
        public long start { get; set; }
        public long end { get; set; }
        public bool allDay { get; set; }
        public string color { get; set; }

        //[ScaffoldColumn(false)]
        //public int contractID { get; set; }
    }
}
