using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlogAgain.Models
{
    [Table("Experiences")]
    public class Experience
    {
        [Key]
        public int ExpId { get; set; }
        public int LocationId { get; set; }
        public string Activity { get; set; }
        public string Description { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Experience_Person> Experience_Persons { get; set; }

    }
}
