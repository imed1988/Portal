using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal.Models.Account
{
    public class UserProfileModel
    {
        [Display(Name ="Full Name:")]
        [Required(ErrorMessage ="Full Name is required")]
        public string FullName { get; set; }

        [Display(Name = "Mail:")]
        [Required(ErrorMessage = "Mail is required")]
        public string Mail { get; set; }

        [Display(Name = "Address:")]
        public string Address { get; set; }

    }
}