using Microsoft.Framework.ConfigurationModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrientDb.Http.Tests
{
	public class DbContextOptionsTests
    {
		private static readonly IConfiguration DummyConfiguration = new Configuration();
		private static readonly IConfiguration NullConfiguration =
			new Configuration(
				new MemoryConfigurationSource(
					new Dictionary<string, string>
					{
						{ "orientDb:http:_default:baseUrl", null },
						{ "orientDb:http:_default:authorization", null },
						{ "orientDb:http:_default:databaseName", null },
					}));
		private static readonly IConfiguration EmptyConfiguration =
			new Configuration(
				new MemoryConfigurationSource(
					new Dictionary<string, string>
					{
						{ "orientDb:http:_default:baseUrl", "" },
						{ "orientDb:http:_default:authorization", "" },
						{ "orientDb:http:_default:databaseName", "" },
					}));
		private static readonly IConfiguration NonUriConfiguration =
			new Configuration(
				new MemoryConfigurationSource(
					new Dictionary<string, string>
					{
						{ "orientDb:http:_default:baseUrl", "non_uri_url" },
						{ "orientDb:http:_default:authorization", "Basic YWRtaW46YWRtaW4=" },
						{ "orientDb:http:_default:databaseName", "test" },
					}));
		private static readonly IConfiguration DefaultConfiguration =
			new Configuration(
				new MemoryConfigurationSource(
					new Dictionary<string, string>
					{
						{ "orientDb:http:_default:baseUrl", "http://localhost:2480" },
						{ "orientDb:http:_default:authorization", "Basic YWRtaW46YWRtaW4=" },
						{ "orientDb:http:_default:databaseName", "test" },
					}));

		[Fact]
		public void GuardClause_all()
		{
			Assert.Throws<ArgumentNullException>(() => new DbContextOptions(null, null));
		}
		[Fact]
		public void GuardClause_configurationRoot()
		{
			Assert.Throws<ArgumentNullException>(() => new DbContextOptions(null, "_default"));
		}
		[Fact]
		public void GuardClause_configurationName()
		{
			Assert.Throws<ArgumentNullException>(() => new DbContextOptions(DummyConfiguration, null));
		}

		[Fact]
		public void Throws_Exception_when_configuration_was_not_found()
		{
			Assert.Throws<ArgumentException>(() => new DbContextOptions(DummyConfiguration));
		}

		[Fact]
		public void Throws_Exception_when_baseUrl_was_null()
		{
			Assert.Throws<InvalidOperationException>(() => new DbContextOptions(NullConfiguration));
		}

		[Fact]
		public void Create_new_instance_with_empty_baseUrl()
		{
			var value = new DbContextOptions(EmptyConfiguration);
			Assert.NotNull(value);
        }

		[Fact]
		public void Throws_Exception_when_baseUrl_was_not_empty_neither_uri()
		{
			Assert.Throws<InvalidOperationException>(() => new DbContextOptions(NonUriConfiguration));
		}

		[Fact]
		public void Create_new_instance_with_appropriate_configuration()
		{
			var value = new DbContextOptions(DefaultConfiguration);
			Assert.NotNull(value);
		}
	}
}
