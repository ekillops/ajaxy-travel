using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlogAgain.Models
{
    [Table("Experience_Person")]
    public class Experience_Person
    {
        [Key]
        public int Experience_PersonId { get; set; }
        public int ExperienceId { get; set; }
        public int PersonId { get; set; }
        public virtual Experience Experience { get; set; }
        public virtual Person Person { get; set; }
        public Experience_Person(int expId, int personId)
        {
            ExperienceId = expId;
            PersonId = personId;
        }

        public Experience_Person()
        {

        }
    }
}
