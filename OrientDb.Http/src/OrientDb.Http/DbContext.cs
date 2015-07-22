using Microsoft.Framework.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OrientDb.Http
{
	public class DbContext : IDbContext
	{
		private readonly IHttpClientProvider clientProvider;
		private readonly DbContextOptions options;

		public DbContext() : this(new DefaultHttpClientProvider(), new DbContextOptions())
		{
		}
		public DbContext(DbContextOptions options) : this(new DefaultHttpClientProvider(), options)
		{
		}
		public DbContext(IHttpClientProvider clientProvider, DbContextOptions options)
		{
			if (clientProvider == null)
			{
				throw new ArgumentNullException(nameof(clientProvider));
			}
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}
			this.clientProvider = clientProvider;
			this.options = options;
			HttpClient = clientProvider.GetHttpClient(options.Configure);
		}

		protected HttpClient HttpClient { get; set; } = new HttpClient();

		public async Task<T> ExecuteCommandAsync<T>(string commandText, CommandOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
			where T : class
		{
			if (commandText == null)
			{
				throw new ArgumentNullException(nameof(commandText));
			}

			await Task.FromResult(0);
			throw new NotImplementedException();
		}
	}
}
