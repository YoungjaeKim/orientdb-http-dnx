using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrientDb.Http
{
	public class HttpClientProvider : IHttpClientProvider
	{
		private static object lockObject = new object();
		private static HttpClient cachedClient;
		public HttpClient GetHttpClient(Action<HttpClient> configure = null)
		{
			lock (lockObject)
			{
				if (cachedClient == null)
				{
					var client = new HttpClient();
					client.DefaultRequestHeaders.Connection.Add("keep-alive");
					client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
					client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
					client.DefaultRequestHeaders.ExpectContinue = false;
					cachedClient = client;
					if (configure != null)
					{
						Task.Factory.StartNew(stateClient =>
						{
							var invoker = stateClient as HttpClient;
							configure(invoker);
						}, client);
					}
				}
			}
			return cachedClient;
		}
	}
}
