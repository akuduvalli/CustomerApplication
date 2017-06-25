using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Models
{
    public partial class License
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LicenseType { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Value { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
