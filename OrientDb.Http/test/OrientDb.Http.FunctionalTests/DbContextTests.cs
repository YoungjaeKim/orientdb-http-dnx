using Microsoft.Framework.ConfigurationModel;
using Newtonsoft.Json;
using OrientDb.Core.FunctionalTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrientDb.Http.FunctionalTests
{
    public class DbContextTests : DbContextTestsBase
    {
		public DbContextTests()
		{
			//var config = new Configuration(source);
			//var dbConfig = config.GetSubKey("orientDb:http:_default");
			//var baseUrl = dbConfig["baseUrl"];
			//var databaseName = dbConfig["databaseName"];
		}

		public override IDbContext GetDefaultDbContext()
		{
			return new DbContext();
		}

		[Fact]
		public void Test()
		{

		}
    }
}
