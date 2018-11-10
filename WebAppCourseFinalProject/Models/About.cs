using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class About
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public About(string title, string address, string email)
        {
            Title = title;
            Address = address;
            this.Email = email;
        }
    }
}
