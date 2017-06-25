using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddress = new HashSet<CustomerAddress>();
            License = new HashSet<License>();
        }

        public Customer(int id,String lastname,String firstname, String email,String phone)
        {
            this.Id = id;
            this.LastName = lastname;
            this.FirstName = firstname;
            this.Email = email;
            this.Phone = phone;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        public string Phone { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
        public virtual ICollection<License> License { get; set; }
    }
}
