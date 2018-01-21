using System.Collections.Generic;
using System.Data.Common;

namespace WeddingWebsiteApi.Configuration
{
    public interface IDatabaseConfiguration
    {
        DbConnection GetSqlConnection();
    }
}