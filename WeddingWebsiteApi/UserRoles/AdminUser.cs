using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPoco;

namespace WeddingWebsiteApi.Authorization
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
    }
}
