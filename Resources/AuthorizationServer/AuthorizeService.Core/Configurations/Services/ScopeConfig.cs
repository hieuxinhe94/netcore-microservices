using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static IdentityModel.OidcConstants;

namespace AuthorizeService.Core.Configurations.Services
{
    public class ScopeConfig
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
        {
            // StandardScopes.OfflineAccess,
            new Scope
            {
                Name = "api1",
                Description = "My API",
                ShowInDiscoveryDocument = true
            }
        };
        }
    }
}
