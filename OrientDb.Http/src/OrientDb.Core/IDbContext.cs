using System.Threading;
using System.Threading.Tasks;

namespace OrientDb
{
	public interface IDbContext
    {
		Task<T> ExecuteCommandAsync<T>(string commandText, CommandOptions options = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
    }
}
