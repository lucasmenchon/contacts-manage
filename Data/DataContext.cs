using ContactsManage.Map;
using ContactsManage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ContactsManage.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
            base.OnModelCreating(modelBuilder);
        }

        public class BancoContextFactory : IDesignTimeDbContextFactory<DataContext>
        {
            public DataContext CreateDbContext(string[] args)
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string environmentSuffix = environmentName == "Development" ? ".Development" : "";
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings{environmentSuffix}.json")
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

                if (environmentName.Equals("Development"))
                {
                    optionsBuilder.UseMySql(configuration.GetConnectionString("LocalDb"), ServerVersion.Parse("8.0.31-mysql"));
                }
                else
                {
                    optionsBuilder.UseMySql(configuration.GetConnectionString("HostDb"), ServerVersion.Parse("8.0.31-mysql"));
                }

                return new DataContext(optionsBuilder.Options);
            }
        }
    }
}