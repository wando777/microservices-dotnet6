using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string DefaultAdminRole = "Admin";
    public const string DefaultUserRole = "User";
    private static IConfiguration? Configuration { get; set; }

    public static void SetConfiguration(IConfiguration? configuration)
    {
        Configuration = configuration;
    }

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("geek_shopping", "GeekShopping Server"),
            new ApiScope("read", "Read data"),
            new ApiScope("write", "Write data"),
            new ApiScope("delete", "Delete data"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets =
                {
                    new Secret(
                        Configuration?["IdentityServer:Clients:SuperSecret"].Sha256()
                        ?? string.Empty
                    )
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "geek_shopping",
                ClientSecrets =
                {
                    new Secret(
                        Configuration?["IdentityServer:Clients:SuperSecret"].Sha256()
                        ?? string.Empty.Sha256()
                    )
                },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:29871/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:29871/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "geek_shopping",
                }
            }
        };
}
