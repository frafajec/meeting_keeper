using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace meeting_keeper.Models
{

    //EACH user has ONLY ONE,
    public class Calendar
    {

        [Key]
        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public long dateCreated { get; set; }
        [ScaffoldColumn(false)]
        public long dateModified { get; set; }
        public string name { get; set; }

        [ForeignKey("AspNetUsers")]
        public string userID { get; set; }

        public bool showSaturday { get; set; }
        public bool showSunday { get; set; }
    }
}
