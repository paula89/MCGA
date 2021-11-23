using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class DataContext : DbContext
    {
        /*Entidades*/
        public Microsoft.EntityFrameworkCore.DbSet<Privilege> Privileges { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> User { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserPrivilege> UsersPrivileges { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RefreshToken> RefreshTokens { get; set; }

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
