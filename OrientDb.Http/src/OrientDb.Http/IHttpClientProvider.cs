using System;
using System.Net.Http;

namespace OrientDb.Http
{
	public interface IHttpClientProvider
    {
		HttpClient GetHttpClient(Action<HttpClient> configure = null);
    }
}
