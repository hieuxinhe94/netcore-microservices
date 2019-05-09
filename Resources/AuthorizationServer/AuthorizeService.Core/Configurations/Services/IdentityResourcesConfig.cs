using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizeService.Core.Configurations.Services
{
    public class IdentityResourcesConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResource()
            };
        }
    }
}
