using OrientDb.Core.FunctionalTests;

namespace OrientDb.Http.FunctionalTests
{
	public class DbContextTests : DbContextTestsBase
    {
		public override IDbContext GetDefaultSut()
		{
			return new DbContext();
		}
    }
}
