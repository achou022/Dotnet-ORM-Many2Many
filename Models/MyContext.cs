using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> AllUsers {get; set;}
        
        public DbSet<Wedding> AllWeddings {get; set;}

        public DbSet<RSVP> AllRSVPs {get; set;}
    }
}