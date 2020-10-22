// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using Azure.Core;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration.AzureAppConfiguration
{
    /// <summary>
    /// Options used to configure the client used to fetch key vault references in an Azure App Configuration provider.
    /// </summary>
    public class AzureAppConfigurationKeyVaultOptions
    {
        internal TokenCredential Credential;
        internal List<SecretClient> SecretClients = new List<SecretClient>();

        /// <summary>
        /// Sets the credentials used to authenticate to key vaults that have no registered <see cref="SecretClient"/>.
        /// </summary>
        /// <param name="credential">Default token credentials.</param>
        public AzureAppConfigurationKeyVaultOptions SetCredential(TokenCredential credential)
        {
            Credential = credential;
            return this;
        }

        /// <summary>
        /// Registers the specified <see cref="SecretClient"/> instance to use to resolve key vault references for secrets from associated key vault.
        /// </summary>
        /// <param name="secretClient">Secret client instance.</param>
        public AzureAppConfigurationKeyVaultOptions Register(SecretClient secretClient)
        {
            SecretClients.Add(secretClient);
            return this;
        }

        /// <summary>
        /// Registers a mock <see cref="SecretClient"/> instance to use to resolve all key vault references to null.
        /// </summary>
        public AzureAppConfigurationKeyVaultOptions ResolveKeyVaultReferencesToNull()
        {
            SecretClients.Add(new MockSecretClient());
            return this;
        }

        /// <summary>
        /// Registers a mock <see cref="SecretClient"/> instance to use to resolve specific key vault references to null.
        /// </summary>
        public AzureAppConfigurationKeyVaultOptions ResolveKeyVaultReferencesToNull(Uri vaultUri)
        {
            SecretClients.Add(new MockSecretClient(vaultUri));
            return this;
        }
    }
}
