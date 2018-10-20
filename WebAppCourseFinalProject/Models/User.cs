using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace WebAppCourseFinalProject.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        [Display(Name = "First Name")]                    
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Mandatory")]
        public string Email { get; set; }

        public string Password { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
    }
}
