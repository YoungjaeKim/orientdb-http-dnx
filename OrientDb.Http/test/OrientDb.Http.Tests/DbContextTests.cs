using Microsoft.Framework.ConfigurationModel;
using OrientDb.Tests;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrientDb.Http.Tests
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

		[Fact]
		public void GuardClause_options()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var value = new DbContext(options: null);
			});
		}

		[Fact]
		public void GuardClause_all()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var value = new DbContext(null, null);
			});
		}
	}
}
