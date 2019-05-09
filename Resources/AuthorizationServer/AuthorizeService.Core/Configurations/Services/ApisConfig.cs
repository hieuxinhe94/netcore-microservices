using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizeService.Core.Configurations.Services
{
    public class ApisConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
    }
}
