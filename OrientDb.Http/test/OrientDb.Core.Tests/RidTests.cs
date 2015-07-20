using Xunit;

namespace OrientDb.Tests
{
	public class RidTests
    {
		[Fact]
		public void New_instance_is_Indeternimate()
		{
			var result = new Rid();
			Assert.True(result.IsIndeterminated);
		}

		[Fact]
		public void Default_is_null()
		{
			var result = default(Rid);
			Assert.Null(result);
		}

		[Fact]
		public void Equals_between_different_references()
		{
			var value = new Rid(0, 0);
			var expected = new Rid(0, 0);
			Assert.True(value == expected);
			Assert.True(expected == value);
			Assert.True(value.Equals(expected));
			Assert.True(expected.Equals(value));
			Assert.Equal(value, expected);
			Assert.Equal(expected, value);
		}
		
		[Theory]
		[InlineData("#0:0")]
		[InlineData("#-0:0")]
		[InlineData("#0:-0")]
		[InlineData("#-0:-0")]
		[InlineData("#0:1")]
		[InlineData("#1:0")]
		[InlineData("#1:1")]
		[InlineData("#12312:12412123")]
		public void Determinatable_rid_string(string rid)
		{
			var result = new Rid(rid);
			Assert.False(result.IsIndeterminated);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("  ")]
		[InlineData(":")]
		[InlineData("#:")]
		[InlineData("#-1")]
		[InlineData("#-1:0")]
		[InlineData("#0:-1")]
		[InlineData("#-1:-1")]
		[InlineData("-1:0")]
		[InlineData("0:-1")]
		[InlineData("-1:-1")]
		[InlineData("#asdf:asdf")]
		[InlineData("invalid_string")]
		[InlineData("#12,312:12,412,123")]
		[InlineData("#12 312:124 12123")]
		[InlineData("-0:0")]
		[InlineData("0:0")]
		[InlineData("0:1")]
		[InlineData("1:1")]
		[InlineData("12312:12412123")]
		public void Indeterminatable_rid_string(string rid)
		{
			var result = new Rid(rid);
			Assert.True(result.IsIndeterminated);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(-0, 0)]
		[InlineData(-0, -0)]
		[InlineData(0, 1)]
		[InlineData(-0, 1)]
		[InlineData(1, 1)]
		[InlineData(12345, 1234567890)]
		public void Deterninatable_rid_values(short clusterId, long rowId)
		{
			var result = new Rid(clusterId, rowId);
			Assert.False(result.IsIndeterminated);
		}

		[Theory]
		[InlineData(-1, 0)]
		[InlineData(0, -1)]
		[InlineData(-1, -1)]
		[InlineData(-12345, -1234567890)]
		public void Indeterninatable_rid_values(short clusterId, long rowId)
		{
			var result = new Rid(clusterId, rowId);
			Assert.True(result.IsIndeterminated);
		}
	}
}
