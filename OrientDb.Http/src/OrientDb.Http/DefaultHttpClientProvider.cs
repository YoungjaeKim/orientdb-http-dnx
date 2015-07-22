using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OrientDb.Http
{
	public class DefaultHttpClientProvider : IHttpClientProvider
	{
		private object lockObject = new object();
		private HttpClient cachedClient;
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
					if (configure != null)
					{
						configure(client);
					}
					cachedClient = client;
				}
			}
			return cachedClient;
		}
	}
}
