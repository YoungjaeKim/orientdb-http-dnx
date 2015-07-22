using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace OrientDb.Http.Tests
{
	public class DefaultHttpClientProviderTests
    {
		public IHttpClientProvider GetSimpleProvider()
		{
			return new DefaultHttpClientProvider();
		}

		[Fact]
		public void New_one_is_IHttpClientProvider()
		{
			var provider = GetSimpleProvider();
			Assert.True(provider is IHttpClientProvider);
		}

		[Fact]
		public void GetHttpClient_doesnt_return_null()
		{
			var provider = GetSimpleProvider();
			var client = provider.GetHttpClient();
			Assert.NotNull(client);
		}

		[Fact]
		public void GetHttpClient_always_returns_same_instance()
		{
			var provider = GetSimpleProvider();
			var client1 = provider.GetHttpClient();
			var client2 = provider.GetHttpClient();
			var client3 = provider.GetHttpClient();
			var client4 = provider.GetHttpClient();
			Assert.True(ReferenceEquals(client1, client2));
			Assert.True(ReferenceEquals(client2, client3));
			Assert.True(ReferenceEquals(client3, client4));
			Assert.True(ReferenceEquals(client4, client1));
		}

		[Fact]
		public void GetHttpClient_always_returns_same_instance_in_different_threads()
		{
			var provider = GetSimpleProvider();
			var tasks = new List<Task<HttpClient>>();
			for (int i = 0; i < 100; i++)
			{
				tasks.Add(Task.Factory.StartNew(() => provider.GetHttpClient()));
			}
			var actual = Task.WhenAll(tasks)
				.ContinueWith(t =>
				{
					var firstClient = t.Result.First();
					return t.Result.All(c => ReferenceEquals(firstClient, c));
				}).Result;
			Assert.True(actual);
		}

		[Fact]
		public void Configuration_remains_for_DefaultRequestHeaders()
		{
			var productInfo = new ProductInfoHeaderValue("OrientDb.Http", "1.0");
			var provider = GetSimpleProvider();
			var client1 = provider.GetHttpClient(c =>
			{
				c.DefaultRequestHeaders.UserAgent.Add(productInfo);
			});
			var client2 = provider.GetHttpClient();
			var actualAgent = client2.DefaultRequestHeaders.UserAgent.First();
            Assert.Equal(productInfo.Product.Name, actualAgent.Product.Name);
			Assert.Equal(productInfo.Product.Version, actualAgent.Product.Version);
		}

		[Fact]
		public void Ignores_secondary_configuration()
		{
			var productInfo = new ProductInfoHeaderValue("OrientDb.Http", "1.0");
			var productInfo2 = new ProductInfoHeaderValue("OrientDb.Http", "2.0");
			var provider = GetSimpleProvider();
			var client1 = provider.GetHttpClient(c =>
			{
				c.DefaultRequestHeaders.UserAgent.Add(productInfo);
			});
			var client2 = provider.GetHttpClient(c =>
			{
				c.DefaultRequestHeaders.UserAgent.Add(productInfo2);
			});
			var actualAgent = client2.DefaultRequestHeaders.UserAgent.First();
			Assert.Equal(productInfo.Product.Name, actualAgent.Product.Name);
			Assert.Equal(productInfo.Product.Version, actualAgent.Product.Version);
		}
	}
}
