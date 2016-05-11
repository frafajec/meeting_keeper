using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace meeting_keeper.Models
{
    public class Client
    {

        [ScaffoldColumn(false)]
        public int id { get; set; }
        [ScaffoldColumn(false)]
        public long dateCreated { get; set; }
        [ScaffoldColumn(false)]
        public long dateModified { get; set; }


        [Required()]
        //[DisplayName("Name")]
        public string name { get; set; }

        //[DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")]
        public string email { get; set; }

        //[DisplayName("Address")]
        public string address { get; set; }

        //[DisplayName("Nbr. contacts")]
        //[ReadOnly(true)]
        public int numberOfContracts { get; set; }

        //[DisplayName("Earliest")]
        //[ReadOnly(true)]
        public long earliestDate { get; set; }

    }
}
