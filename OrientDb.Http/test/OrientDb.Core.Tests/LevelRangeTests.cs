using System;
using Xunit;

namespace OrientDb.Tests
{
	public class LevelRangeTests
    {
		[Fact]
		public void GuardClause()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var value = new LevelRange(null);
			});
		}

		[Fact]
		public void New_instance_is_from_0_to_max()
		{
			var value = new LevelRange();
			Assert.True(value.From == 0);
			Assert.True(value.To == ushort.MaxValue);
			Assert.True(value.IsValid);
		}

		[Fact]
		public void New_instance_ToString_returns_star()
		{
			var value = new LevelRange().ToString();
			var expected = "*";
			Assert.Equal(expected, value);
		}

		[Fact]
		public void Invalid_range()
		{
			var value = new LevelRange(5, 1);
			Assert.False(value.IsValid);
		}

		[Theory]
		[InlineData("a")]
		[InlineData("--")]
		[InlineData("-1-")]
		[InlineData("-*-")]
		[InlineData("-12345678")]
		[InlineData("12345678-")]
		public void Invalid_initialization_throws_ArgumentException(string range)
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var value = new LevelRange(range);
			});
		}

		[Theory]
		[InlineData(0, ushort.MaxValue, "*")]
		[InlineData(0, 0, "0")]
		[InlineData(3, 3, "3")]
		[InlineData(0, 3, "0-3")]
		[InlineData(2, 4, "2-4")]
		[InlineData(2, ushort.MaxValue, "2-")]
		public void Validate_from_to(ushort from, ushort to, string expected)
		{
			var value = new LevelRange(from, to).ToString();
			Assert.Equal(expected, value);
		}

		[Theory]
		[InlineData(0, ushort.MaxValue, "*")]
		[InlineData(0, 0, "0")]
		[InlineData(3, 3, "3")]
		[InlineData(0, 3, "0-3")]
		[InlineData(0, 3, "-3")]
		[InlineData(2, 4, "2-4")]
		[InlineData(2, ushort.MaxValue, "2-")]
		public void Validate_string(ushort from, ushort to, string input)
		{
			var value = new LevelRange(input);
			Assert.Equal(from, value.From);
			Assert.Equal(to, value.To);
			Assert.True(value.IsValid);
		}
	}
}
