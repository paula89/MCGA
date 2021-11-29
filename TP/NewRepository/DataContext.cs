using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class DataContext : DbContext
    {
        /*Entidades*/
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserPrivilege> UsersPrivileges { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Package> Packages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                var configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LPPTEntities"));
            }
        }
    }
}
