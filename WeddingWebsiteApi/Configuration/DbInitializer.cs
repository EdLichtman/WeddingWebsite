using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeddingWebsiteApi.Configuration
{
    public static class DbInitializer
    {

        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var config = services.GetService<IConfiguration>();

            var defaultDatabase = context.Database.GetDbConnection().ConnectionString;

            SetConnectionFor(context, config.GetConnectionString("Sys"));
            context.Database.ExecuteSqlCommand(CreateDbScript);

            SetConnectionFor(context, defaultDatabase);

            var initializeDbScript = GetInitializeDbScript(config);
            context.Database.ExecuteSqlCommand(initializeDbScript);
        }
        private const string CreateDbScript = @"
CREATE DATABASE IF NOT EXISTS weddingwebsite2
    CHARACTER SET latin1;
";
        private static string GetInitializeDbScript(IConfiguration config)
        {
            const string createUserCommand = "CREATE USER IF NOT EXISTS '{0}'@'localhost' IDENTIFIED BY '{1}';";
            string createUsersCommand = "";
            var connectionStrings = config.AsEnumerable().Where(m => m.Key.StartsWith("ConnectionString"));
            foreach(var connectionString in connectionStrings)
            {
                if (!string.IsNullOrWhiteSpace(connectionString.Value))
                {
                    var properties = connectionString.Value.Split(';');
                    var initialCatalog = properties.First(m => m.StartsWith("Database="));
                    if (!initialCatalog.ToLower().Equals("database=sys"))
                    {
                        var userName = properties.First(m => m.StartsWith("Uid=")).Replace("Uid=", "");
                        var password = properties.First(m => m.StartsWith("Pwd=")).Replace("Pwd=", "");
                        createUsersCommand += String.Format(createUserCommand, userName, password);
                    }
                }
            }
            return string.Format(InitializeDbScript, createUsersCommand);
        }
        private const string InitializeDbScript = @"
{0}
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
";
        private static void SetConnectionFor(ApplicationDbContext context, string connectionString)
        {
            context.Database.GetDbConnection().ConnectionString = connectionString;
            
        }
    }
}
