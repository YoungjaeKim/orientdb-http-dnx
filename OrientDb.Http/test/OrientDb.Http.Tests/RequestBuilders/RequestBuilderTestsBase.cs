using OrientDb.Http.RequestBuilders;
using System;
using Xunit;

namespace OrientDb.Http.Tests.RequestBuilders
{
	public abstract class RequestBuilderTestsBase<TPayload> where TPayload : class
	{
		public abstract RequestBuilder<TPayload> CreateNullParameteredSut();
		public abstract RequestBuilder<TPayload> CreateDefaultSut();

		[Fact]
		public virtual void GuardClause()
		{
			Assert.Throws<ArgumentNullException>(() => CreateNullParameteredSut());
		}

		[Fact]
		public virtual void GuardClause_GetMessage()
		{
			var sut = CreateDefaultSut();
			Assert.Throws<ArgumentNullException>(() => sut.GetMessage(null));
		}
	}

	public abstract class RequestBuilderTestsBase<TPayload, TOptions>
		where TPayload : class
		where TOptions : class
	{
		public abstract RequestBuilder<TPayload, TOptions> CreateNullParameteredSut();
		public abstract RequestBuilder<TPayload, TOptions> CreateDefaultSut();

		[Fact]
		public virtual void GuardClause()
		{
			Assert.Throws<ArgumentNullException>(() => CreateNullParameteredSut());
		}

		[Fact]
		public virtual void GuardClause_GetMessage()
		{
			var sut = CreateDefaultSut();
			Assert.Throws<ArgumentNullException>(() => sut.GetMessage(null));
		}

		[Fact]
		public virtual void GuardClause_GetBaseUrl()
		{
			var sut = CreateDefaultSut();
			Assert.Throws<ArgumentNullException>(() => sut.GetBaseUrl(null));
        }
	}
}
