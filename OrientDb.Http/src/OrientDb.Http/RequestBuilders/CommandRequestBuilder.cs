using System;
using System.Net.Http;

namespace OrientDb.Http.RequestBuilders
{
	public class CommandRequestBuilder : RequestBuilder<string, CommandOptions>
	{
		public CommandRequestBuilder(DbContextOptions options) : base(options)
		{
		}

		public override string ApiName { get; } = "command";

		public override HttpRequestMessage GetMessage(string payload)
		{
			return GetMessage(payload, new CommandOptions());
		}

		public override HttpRequestMessage GetMessage(string payload, CommandOptions requestOptions)
		{
			if (payload == null)
			{
				throw new ArgumentNullException(nameof(payload));
			}

			var url = GetBaseUrl(requestOptions);
			var request = new HttpRequestMessage(HttpMethod.Post, url);
			request.Content = new StringContent(payload);
			request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
			return request;
		}

		public override string GetBaseUrl(CommandOptions requestOptions)
		{
			return base.GetBaseUrl(requestOptions)
				+ $"/{requestOptions.Language.ToString().ToLower()}/{requestOptions.Limit}/{requestOptions.FetchPlan.ToString()}";
		}
	}
}
