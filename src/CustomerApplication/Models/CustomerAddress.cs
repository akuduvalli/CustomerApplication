using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Models
{
    public partial class CustomerAddress
    {
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Required")]
        public int StateorProvince { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Country { get; set; }
        public int CustomerId { get; set; }

        public virtual CountryMaster CountryNavigation { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
