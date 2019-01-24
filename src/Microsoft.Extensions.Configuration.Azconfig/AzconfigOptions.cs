﻿namespace Microsoft.Extensions.Configuration.Azconfig
{
    using Microsoft.Azconfig.Client;
    using Microsoft.Extensions.Configuration.Azconfig.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AzconfigOptions
    {
        private Dictionary<string, KeyValueWatcher> _changeWatchers = new Dictionary<string, KeyValueWatcher>();

        private List<KeyValueSelector> _kvSelectors = new List<KeyValueSelector>();

        private readonly TimeSpan _defaultPollInterval = TimeSpan.FromSeconds(30);

        public IEnumerable<KeyValueSelector> KeyValueSelectors => _kvSelectors;

        /// <summary>
        /// The connection string to use to connect to the configuration store.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// An optional client that can be used to communicate with the configuration store. If provided, connection string will be ignored.
        /// </summary>
        public AzconfigClient Client { get; set; }

        public IEnumerable<KeyValueWatcher> ChangeWatchers
        {
            get
            {
                return _changeWatchers.Values;
            }
        }

        /// <summary>
        /// Instructs the AzconfigOptions to poll the key-values with matching the specified key in every 30s interval. 
        /// </summary>
        /// <param name="key">
        /// The key used for querying the configuration store for key-values.
        /// </param>
        public AzconfigOptions Watch(string key)
        {
            return Watch(key, "", _defaultPollInterval);
        }

        /// <summary>
        /// Instructs the AzconfigOptions to poll the key-values with matching the specified key with customized polling interval. 
        /// </summary>
        /// <param name="key">
        /// The key used for querying the configuration store for key-values.
        /// </param>
        /// <param name="pollInterval">
        /// The interval used to poll query the configuration store.
        /// </param>
        public AzconfigOptions Watch(string key, TimeSpan pollInterval)
        {
            return Watch(key, "", pollInterval);
        }

        /// <summary>
        /// Instructs the AzconfigOptions to poll the key-values with matching the specified key and label in every 30s interval. 
        /// </summary>
        /// <param name="key">
        /// The key used for querying the configuration store for key-values.
        /// </param>
        /// <param name="label">
        /// The label used for querying the configuration store for key-values.
        /// </param>
        public AzconfigOptions Watch(string key, string label)
        {
            return Watch(key, label, _defaultPollInterval);
        }

        /// <summary>
        /// Instructs the AzconfigOptions to poll the key-values with matching the specified key and label with customized polling interval. 
        /// </summary>
        /// <param name="key">        
        /// The key used for querying the configuration store for key-values.
        /// </param>
        /// <param name="label">
        /// The label used for querying the configuration store for key-values.
        /// </param>
        /// <param name="pollInterval">
        /// The interval used to poll query the configuration store.
        /// </param>
        public AzconfigOptions Watch(string key, string label, TimeSpan pollInterval)
        {
            _changeWatchers[key] = new KeyValueWatcher()
            {
                Key = key,
                Label = label,
                PollInterval = pollInterval
            };
            return this;
        }

        /// <summary>
        /// Instructs the AzconfigOptions to include all key-values with matching the specified key and label filters.
        /// </summary>
        /// <param name="keyFilter">
        /// The key filter to apply when querying the configuration store for key-values.
        /// </param>
        /// <param name="labelFilter">
        /// The label filter to apply when querying the configuration store for key-values.
        /// Does not support '*' and ','.
        /// </param>
        /// <param name="preferredDateTime">
        /// Used to query key-values in the state that they existed at the time provided.
        /// </param>
        public AzconfigOptions Use(string keyFilter, string labelFilter = null, DateTimeOffset? preferredDateTime = null)
        {
            if (string.IsNullOrEmpty(keyFilter))
            {
                throw new ArgumentNullException(nameof(keyFilter));
            }

            if (labelFilter == null)
            {
                labelFilter = string.Empty;
            }

            // Do not support * and , for label filter for now.
            if (labelFilter.Contains('*') || labelFilter.Contains(','))
            {
                throw new ArgumentException("The characters '*' and ',' are not supported in label filters.", nameof(labelFilter));
            }

            var keyValueSelector = new KeyValueSelector()
            {
                KeyFilter = keyFilter,
                LabelFilter = labelFilter,
                PreferredDateTime = preferredDateTime
            };

            _kvSelectors.Add(keyValueSelector);

            return this;
        }

        public AzconfigOptions Connect(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }
    }
}
