using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlClient;

namespace WeddingWebsiteApi.Configuration
{
    public class DefaultDatabaseConfiguration : IDatabaseConfiguration
    {
        public DefaultDatabaseConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetSection("ConnectionStrings:Root").Value;
            DatabaseType = DatabaseTypeConnectors.MySql;
        }
        public string ConnectionString { get; set; }
        public string DatabaseType { get; set; } 

        public DbConnection GetSqlConnection()
        {
            if (DatabaseType == DatabaseTypeConnectors.MySql)
            {
                return new MySqlConnection(ConnectionString);
            }
            if (DatabaseType == DatabaseTypeConnectors.SqlServer)
            {
                return new SqlConnection(ConnectionString);
            }
            throw new NotImplementedException();
        }
    }
    
    public static class DatabaseTypeConnectors
    {
        public const string MySql = "MySql.Data.MySqlClient";
    //public const string MySql = "System.Data.MySqlClient";
        public const string SqlServer = "System.Data.SqlClient";
    }
}
