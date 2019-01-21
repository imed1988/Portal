using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal.Models.Account
{
    public class RegisterModel
    {
        [Display(Name ="Full Name :")]
        [Required(ErrorMessage ="Full Name is Required")]
        public string FullName { get; set; }

        [Display(Name = "User Name :")]
        [Required(ErrorMessage = "User Name is Required")]
        public string Username { get; set; }

        [Display(Name = "Passowrd :")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Passowrd :")]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(otherProperty:"Password", ErrorMessage ="Password doesn't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Mail :")]
        [Required(ErrorMessage = "Mail is Required")]
        [EmailAddress(ErrorMessage ="Mail Required")]
        public string Mail { get; set; }

        [Display(Name = "Confirm Mail :")]
        [Required(ErrorMessage = "Confirm Mail is Required")]
        [EmailAddress(ErrorMessage = "Mail Required")]
        [Compare(otherProperty: "Mail", ErrorMessage = "Mail doesn't match")]
        public string ConfirmMail { get; set; }

        [Display(Name = "Role :")]
        [Required(ErrorMessage = "Role is Required")]
        [UIHint("RolesComboBox")]
        public string Role { get; set; }

    }
}