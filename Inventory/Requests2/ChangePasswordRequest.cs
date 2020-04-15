using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Requests
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage ="Current Password is required!")]
        public string curPassword { get; set; }


        [Required(ErrorMessage ="New Password is required!")]
        public string newPassword { get; set; }
        
        [Required(ErrorMessage ="Confirm Password is required!")]
        public string conPassword { get; set; }
      
    }
}