using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string DefaultAdminRole = "Admin";
    public const string DefaultUserRole = "User";

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("geek_shopping", "GeekShopping Server"),
        new ApiScope("read", "Read data"),
        new ApiScope("write", "Write data"),
        new ApiScope("delete", "Delete data"),
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "client",
            ClientSecrets = { new Secret("my_super_secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "read", "write", "profile" }
        }
    };
}
