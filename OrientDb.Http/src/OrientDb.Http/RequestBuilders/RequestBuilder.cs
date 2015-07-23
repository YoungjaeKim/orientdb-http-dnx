using System;
using System.Net.Http;

namespace OrientDb.Http.RequestBuilders
{
	public abstract class RequestBuilder<TPayload> where TPayload : class
    {
		private readonly DbContextOptions options;
		public RequestBuilder(DbContextOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}
			if (options.BaseUrl == null)
			{
				throw new ArgumentNullException(nameof(options.BaseUrl));
			}
			this.options = options;
		}

		public abstract string ApiName { get; }
		public abstract HttpRequestMessage GetMessage(TPayload payload);
		public virtual string GetBaseUrl()
		{
			var baseUrl = options.BaseUrl.EndsWith("/") ? options.BaseUrl : options.BaseUrl + "/";
            return $"{baseUrl}{ApiName}/{options.DatabaseName}";
        }
	}
	public abstract class RequestBuilder<TPayload, TOptions> : RequestBuilder<TPayload> 
		where TPayload : class
		where TOptions : class
	{
		protected RequestBuilder(DbContextOptions options) : base(options)
		{
		}

		public abstract HttpRequestMessage GetMessage(TPayload payload, TOptions requestOptions);
		public virtual string GetBaseUrl(TOptions requestOptions)
		{
			if (requestOptions == null)
			{
				throw new ArgumentNullException(nameof(requestOptions));
			}
			return GetBaseUrl();
		}
	}
}
