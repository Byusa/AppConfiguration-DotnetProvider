// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
namespace Microsoft.Extensions.Configuration.AzureAppConfiguration
{
    /// <summary>
    /// Defines well known Key Vault reference filters that are used within Azure App Configuration.
    /// </summary>
    public class KeyVaultReferenceFilter
    {
        /// <summary>
        /// The filter that matches Key Vault references with any VaultUri.
        /// </summary>
        public const string Any = "*";
    }
}
