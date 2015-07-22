using Microsoft.Framework.ConfigurationModel;
using OrientDb.Core.FunctionalTests;
using System.Collections.Generic;

namespace OrientDb.Http.FunctionalTests
{
	public class DbContextTests : DbContextTestsBase
    {
		// in aspnet beta5, JsonConfigurationSource doesn't work.
		// a hard coded configuration will be used before beta6.
		private static readonly IConfiguration DefaultConfiguration = 
			new Configuration(
				new MemoryConfigurationSource(
					new Dictionary<string, string>
					{
						{ "orientDb:http:_default:baseUrl", "http://localhost:2480" },
						{ "orientDb:http:_default:authorization", "Basic YWRtaW46YWRtaW4=" },
						{ "orientDb:http:_default:databaseName", "test" },
					}));

		public override IDbContext GetDefaultSut()
		{
			return new DbContext(new DbContextOptions(DefaultConfiguration));
		}
    }
}
