using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class SearchModel
    {
        public int? WriterId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
