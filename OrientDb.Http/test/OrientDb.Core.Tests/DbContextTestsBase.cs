using System;
using Xunit;

namespace OrientDb.Tests
{
	public abstract class DbContextTestsBase
    {
		public abstract IDbContext GetDefaultSut();

		[Fact]
		public virtual void DbContext_is_IDbContext()
		{
			var dbContext = GetDefaultSut();
			Assert.True(dbContext is IDbContext);
		}

		[Fact]
		public virtual void ExecuteCommandAsync_GuardClause()
		{
			var dbContext = GetDefaultSut();
			Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await dbContext.ExecuteCommandAsync<object>(null);
			}).Wait();
		}

	}
}
