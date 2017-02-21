using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlogAgain.Models
{
    [Table("Suggestions")]
    public class Suggestion
    {
        [Key]
        public int Id { get; set; }
        public virtual Location Location { get; set; }
        private int Votes;

        public int GetVotes()
        {
            return Votes;
        }
        public void UpVote()
        {
            Votes += 1;
        }
    }
}
