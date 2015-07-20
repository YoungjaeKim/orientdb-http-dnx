using System;
using System.Threading.Tasks;
using Xunit;

namespace OrientDb.Core.FunctionalTests
{
	public abstract class DbContextTestsBase
    {
		public abstract IDbContext GetDefaultDbContext();

		[Fact]
		public async Task GuardClause()
		{
			var dbContext = GetDefaultDbContext();
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await dbContext.ExecuteCommandAsync<object>(null);
			});
		}
    }
}
