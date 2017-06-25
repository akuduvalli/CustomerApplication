using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Models
{
    public partial class CustomerViewModel
    {
        [RegularExpression(@"^$|^[0-9]*$")]
        public int? Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        public string Phone { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public int? StateorProvince { get; set; }
        public int? Country { get; set; }

        public string LicenseType { get; set; }
        public string Value { get; set; }
    }
}
