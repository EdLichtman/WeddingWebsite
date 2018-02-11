using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingWebsiteApi.Authorization
{
    public class JwtConstants
    {
        public class Claims
        {
            public const string ApiAccess = "api_access";
        }
        public class ClaimIdentifiers
        {
            public const string Rol = "rol", Id = "id";
        }
    }
}
