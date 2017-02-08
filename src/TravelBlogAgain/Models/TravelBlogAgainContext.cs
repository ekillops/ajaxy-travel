using Microsoft.EntityFrameworkCore;


namespace TravelBlogAgain.Models
{
    public class TravelBlogAgainContext : DbContext
    {
        public TravelBlogAgainContext()
        {

        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Experience_Person> Experience_Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TravelBlogAgain;integrated security=True;");
        }

        public TravelBlogAgainContext(DbContextOptions<TravelBlogAgainContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}