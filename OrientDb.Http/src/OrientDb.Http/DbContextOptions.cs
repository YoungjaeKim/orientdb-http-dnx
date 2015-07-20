using Microsoft.Framework.ConfigurationModel;
using System;

namespace OrientDb.Http
{
	public class DbContextOptions
    {
		private const string ConfigurationRootPrefix = "orientDb:http:";
		private const string DefaultConfigurationName = "_default";

		public DbContextOptions()
		{
		}
		public DbContextOptions(IConfiguration configurationRoot) : this(configurationRoot, DefaultConfigurationName)
		{
		}
        public DbContextOptions(IConfiguration configurationRoot, string configurationName)
		{
			if (configurationRoot == null)
			{
				throw new ArgumentNullException(nameof(configurationRoot));
			}
			if (configurationName == null)
			{
				throw new ArgumentNullException(nameof(configurationName));
			}

			var configuration = configurationRoot.GetSubKey($"{ConfigurationRootPrefix}{configurationName}");
            if (configuration == null && !configurationName.Equals(DefaultConfigurationName, StringComparison.OrdinalIgnoreCase))
			{
				configuration = configurationRoot.GetSubKey($"{ConfigurationRootPrefix}{DefaultConfigurationName}");
            }
			if (configuration == null)
			{
				throw new ArgumentException(nameof(configurationName));
			}

			BaseUrl = configuration["baseUrl"];
			if (BaseUrl == null)
			{
				throw new InvalidOperationException($"baseUrl must not be null.");
			}
			if (!BaseUrl.EndsWith("/"))
			{
				BaseUrl += "/";
			}
			Uri uri;
			if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out uri) || !uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException($"Invalid baseUrl configuration. baseUrl '{BaseUrl}' must be an absolute url.");
			}
			DatabaseName = configuration["databaseName"] ?? configurationName;
			Authorization = configuration["authorization"] ?? "";
		}

		public string BaseUrl { get; set; }
		public string DatabaseName { get; set; }
		public string Authorization { get; set; }
	}
}
