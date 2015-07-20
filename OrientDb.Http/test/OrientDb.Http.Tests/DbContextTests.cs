using OrientDb.Tests;

namespace OrientDb.Http.Tests
{
	public class DbContextTests : DbContextTestsBase
	{
		public override IDbContext GetSimpleDbContext()
		{
			return new DbContext();
		}
	}
}
