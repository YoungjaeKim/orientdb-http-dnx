using Microsoft.Framework.Configuration;
using System;
using System.Linq;
using System.Net.Http;

namespace OrientDb.Http
{
	public class DbContextOptions
    {
		private const string ConfigurationRootPrefix = "orientDb:http:";
		private const string DefaultConfigurationName = "_default";

		private static IConfiguration GetDefaultConfiguration()
		{
			var builder = new ConfigurationBuilder(".");
            builder.AddJsonFile("config.json", true);
            return builder.Build();
		}

		public DbContextOptions() : this(GetDefaultConfiguration())
		{
		}
		public DbContextOptions(IConfiguration configurationRoot) : this(configurationRoot, DefaultConfigurationName)
		{
		}
        public DbContextOptions(IConfiguration configurationRoot, string configurationName)
		{
			Load(configurationRoot, configurationName);
		}

		public void Load(IConfiguration configurationRoot, string configurationName)
		{
			if (configurationRoot == null)
			{
				throw new ArgumentNullException(nameof(configurationRoot));
			}
			if (configurationName == null)
			{
				throw new ArgumentNullException(nameof(configurationName));
			}

			var configuration = configurationRoot.GetConfigurationSection($"{ConfigurationRootPrefix}{configurationName}");
			
			if (!configuration.GetConfigurationSections().Any()
				&& !configurationName.Equals(DefaultConfigurationName, StringComparison.OrdinalIgnoreCase))
			{
				configuration = configurationRoot.GetConfigurationSection($"{ConfigurationRootPrefix}{DefaultConfigurationName}");
			}
			if (!configuration.GetConfigurationSections().Any())
			{
				throw new ArgumentException(nameof(configurationName));
			}

			BaseUrl = configuration["baseUrl"];
			if (BaseUrl == null)
			{
				throw new InvalidOperationException($"baseUrl must not be null.");
			}
			if (!string.IsNullOrEmpty(BaseUrl))
			{
				Uri uri;
				if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out uri) || !uri.IsAbsoluteUri)
				{
					throw new InvalidOperationException($"Invalid baseUrl configuration. baseUrl '{BaseUrl}' must be an absolute url.");
				}
				BaseUrl = uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
			}
			DatabaseName = configuration["databaseName"] ?? configurationName;
			Authorization = configuration["authorization"] ?? "";
		}

		public string BaseUrl { get; set; }
		public string DatabaseName { get; set; }
		public string Authorization { get; set; }
		public Action<HttpClient> Configure { get; set; }
	}
}
