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
		public DbContext()
		{

		}
		public DbContext(IHttpClientProvider clientProvider, DbContextOptions options)
		{
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
