using System;
using System.Collections.Generic;

namespace CustomerApplication.Models
{
    public partial class CustomerAddressViewModel
    {

        public int AddressId { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string StateorProvince { get; set; }
        public string Country { get; set; }
    }
}
