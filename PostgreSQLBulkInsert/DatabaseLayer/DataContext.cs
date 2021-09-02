using Microsoft.EntityFrameworkCore;

namespace PostgreSQLBulkInsert
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Dependencies.PostgresConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Address>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().
                HasIndex(b => b.SecondName);

            modelBuilder.Entity<Person>().
                HasMany(x => x.Addresses).
                WithOne(x => x.Person).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}