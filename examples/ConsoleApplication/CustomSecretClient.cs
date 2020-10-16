using Azure;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class CustomSecretClient : SecretClient
    {
        class MockResponse<T> : Response<T>
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
            
        public CustomSecretClient(Uri vaultUri)
        {
            VaultUri = vaultUri ?? new Uri("https://test.vault.azure.net");
        }

        public override Task<Response<KeyVaultSecret>> GetSecretAsync(string name, string version = null, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((Response<KeyVaultSecret>)new MockResponse<KeyVaultSecret>(null));
        }
    }
}
