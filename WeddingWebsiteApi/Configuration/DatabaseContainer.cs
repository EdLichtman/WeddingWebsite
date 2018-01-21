using Microsoft.Extensions.Options;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using MySql.Data.MySqlClient;

namespace WeddingWebsiteApi.Configuration
{
    public class DatabaseContainer : IDatabaseContainer
    {
        public IDatabase Database { get; private set; }

        public DatabaseContainer(IDatabaseConfiguration databaseConfiguration)
        {
            var connection = databaseConfiguration.GetSqlConnection();
            connection.Open();
            Database = new Database(connection);
        }
    }

    public interface IDatabaseContainer
    {
        IDatabase Database { get; }
    }
}
