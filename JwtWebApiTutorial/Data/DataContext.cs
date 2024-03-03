using JwtWebApiTutorial.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<UserDetails> UserDetailTb { get; set; }
        public DbSet<User> UserTb { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbSeedInitializer(modelBuilder).Seed();
        }

    }

}
