using System;
using System.Collections.Generic;

namespace CustomerApplication.Models
{
    public partial class CountryMaster
    {
        public CountryMaster()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            StateMaster = new HashSet<StateMaster>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<StateMaster> StateMaster { get; set; }
    }
}
