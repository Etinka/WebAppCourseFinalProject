using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameForDisplay { get { return "#" + Name.Trim() + " "; } }

    }
}
