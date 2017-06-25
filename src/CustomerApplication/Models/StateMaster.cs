using System;
using System.Collections.Generic;

namespace CustomerApplication.Models
{
    public partial class StateMaster
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int? CountryId { get; set; }

        public virtual CountryMaster Country { get; set; }
    }
}
