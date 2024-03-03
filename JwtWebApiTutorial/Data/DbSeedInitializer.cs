using JwtWebApiTutorial.Entities;

namespace JwtWebApiTutorial.Data
{
    public class DbSeedInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbSeedInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<UserDetails>().HasData(
                   new UserDetails() { Id = 1 , FirstName = "Samad" , LastName = "Hasanpour" , Name = "Smd" , Place = "Tabriz" },
                   new UserDetails() { Id = 2 , FirstName = "Sadra" , LastName = "Hasanpour" , Name = "Sdr" , Place = "Tabriz" },
                   new UserDetails() { Id = 3 , FirstName = "Ali" , LastName = "Mousavi" , Name = "Alm" , Place = "Tabriz" }
    

            );
        }
    }
}
