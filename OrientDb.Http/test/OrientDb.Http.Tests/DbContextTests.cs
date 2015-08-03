using OrientDb.Tests;
using System;
using Xunit;

namespace OrientDb.Http.Tests
{
	public class DbContextTests : DbContextTestsBase
	{
		public override IDbContext GetDefaultSut()
		{
			return new DbContext();
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
