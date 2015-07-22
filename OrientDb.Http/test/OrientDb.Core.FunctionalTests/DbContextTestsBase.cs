using System;
using System.Threading.Tasks;
using Xunit;

namespace OrientDb.Core.FunctionalTests
{
	public abstract class DbContextTestsBase
    {
		public abstract IDbContext GetDefaultSut();

		[Fact]
		public async Task GuardClause()
		{
			var dbContext = GetDefaultSut();
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await dbContext.ExecuteCommandAsync<object>(null);
			});
		}
    }
}
