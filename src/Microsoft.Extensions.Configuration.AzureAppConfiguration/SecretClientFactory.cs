using Azure.Security.KeyVault.Secrets;
using System;

namespace Microsoft.Extensions.Configuration.AzureAppConfiguration
{
    /// <summary>
    /// Secret Client factory that enables mocking SecretClient.
    /// </summary>
    public static class SecretClientFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="SecretClient"/> which resolves all Key Vault references to null.
        /// </summary>
        public static SecretClient GetMockSecretClient()
        {
            return new MockSecretClient();
        }

        /// <summary>
        /// Creates a new instance of <see cref="SecretClient"/> which resolves specific Key Vault references to null.
        /// </summary>
        /// <param name="vaultUri">The Key Vault reference <see cref="Uri"/> that should be resolved to null.</param>
        public static SecretClient GetMockSecretClient(Uri vaultUri)
        {
            return new MockSecretClient(vaultUri);
        }
    }
}
