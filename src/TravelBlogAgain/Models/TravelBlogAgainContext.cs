using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace TravelBlogAgain.Models
{
    public class TravelBlogAgainContext : IdentityDbContext<ApplicationUser>
    {

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Experience_Person> Experience_Person { get; set; }
        public virtual DbSet<Suggestion> Suggestions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TravelBlogAgain;integrated security=True;");
        //}

        public TravelBlogAgainContext(DbContextOptions options): base(options)
        {

        }

        public TravelBlogAgainContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}