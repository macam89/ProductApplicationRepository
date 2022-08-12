using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccessLayer.AppDBContext
{
    public class AppContext : DbContext
    {

        public static string _connectionString;


        public AppContext()
        {
            Database.SetCommandTimeout(1000);
        }


        public DbSet<Product> Product { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                ReadConnectionString();
            }

            optionsBuilder.UseSqlServer(_connectionString);
        }


        private static void ReadConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("AppDBConnectionString");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("ProductDB");
        }


    }
}
