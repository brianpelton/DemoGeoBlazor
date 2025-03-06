using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Gis.BlazorServerApp;

public static class ProgramExtensions
{
    public static void AddSecretToConfiguration(this WebApplicationBuilder builder, string secretName, string? configurationName = null)
    {
        string keyVaultUrl = Environment.GetEnvironmentVariable("KEY_VAULT_URI");

        // Authenticate using Managed Identity
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

        // Retrieve the secret
        KeyVaultSecret secret = client.GetSecret(secretName);
        string apiKey = secret.Value;
        builder.Configuration[configurationName ?? secretName] = secret.Value;
    }
}
