using System;
using Xunit;

namespace OrientDb.Tests
{
	public abstract class DbContextTestsBase
    {
		public abstract IDbContext GetSimpleDbContext();

		[Fact]
		public virtual void DbContext_is_IDbContext()
		{
			var dbContext = GetSimpleDbContext();
			Assert.True(dbContext is IDbContext);
		}

		[Fact]
		public virtual void ExecuteCommandAsync_GuardClause()
		{
			var dbContext = GetSimpleDbContext();
			Assert.Throws<ArgumentNullException>(() =>
			{
				dbContext.ExecuteCommandAsync<object>(null);
			});
		}

	}
}
