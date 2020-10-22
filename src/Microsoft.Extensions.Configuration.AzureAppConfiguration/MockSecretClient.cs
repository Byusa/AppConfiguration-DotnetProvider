using Azure;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration.AzureAppConfiguration
{
    internal class MockSecretClient : SecretClient
    {
        private class MockResponse<T> : Response<T>
        {
            private T _value;
            public override T Value => _value;

            public MockResponse(T value)
            {
                _value = value;
            }

            public override Response GetRawResponse()
            {
                throw new NotImplementedException();
            }
        }

        public override Uri VaultUri { get; }

        /// <summary>
        /// SecretClient for resolving all Key Vault references to null
        /// </summary>
        public MockSecretClient() { }

        /// <summary>
        /// SecretClient for resolving specific Key Vault references to null
        /// </summary>
        public MockSecretClient(Uri vaultUri)
        {
            VaultUri = vaultUri;
        }

        public override Task<Response<KeyVaultSecret>> GetSecretAsync(string name, string version = null, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((Response<KeyVaultSecret>)new MockResponse<KeyVaultSecret>(null));
        }
    }
}
