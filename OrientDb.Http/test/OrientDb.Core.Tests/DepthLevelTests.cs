using Xunit;

namespace OrientDb.Tests
{
	public class DepthLevelTests
    {
		[Fact]
		public void Default_value_is_zero()
		{
			var value = new DepthLevel();
			Assert.True(value.Value == 0);
		}

		[Fact]
		public void Default_level_is_Level()
		{
			var value = new DepthLevel();
			Assert.True(value.Level == DepthLevel.LevelType.Level);
		}

		[Fact]
		public void Unlimited_value_is_minus_1()
		{
			var value = DepthLevel.Unlimited;
			Assert.True(value.Value == -1);
		}

		[Fact]
		public void Unlimited_level_is_Unlimited()
		{
			var value = DepthLevel.Unlimited;
			Assert.True(value.Level == DepthLevel.LevelType.Unlimited);
		}

		[Fact]
		public void Excluded_value_is_minus_2()
		{
			var value = DepthLevel.Excluded;
			Assert.True(value.Value == -2);
		}

		[Fact]
		public void Excluded_level_is_Excluded()
		{
			var value = DepthLevel.Excluded;
			Assert.True(value.Level == DepthLevel.LevelType.Excluded);
		}

		[Fact]
		public void Zero_value_equals_to_default()
		{
			var value = new DepthLevel(0);
			var expected = default(DepthLevel);
			Assert.True(value == expected);
			Assert.Equal(value, expected);
		}

		[Fact]
		public void Minus_1_value_equals_to_Unlimited()
		{
			var value = new DepthLevel(-1);
			var expected = DepthLevel.Unlimited;
			Assert.True(value == expected);
			Assert.Equal(value, expected);
		}

		[Fact]
		public void Minus_2_value_equals_to_Excluded()
		{
			var value = new DepthLevel(-2);
			var expected = DepthLevel.Excluded;
			Assert.True(value == expected);
			Assert.Equal(value, expected);
		}

		[Theory]
		[InlineData(-3, "-2")]
		[InlineData(-2, "-2")]
		[InlineData(-1, "-1")]
		[InlineData(-0, "0")]
		[InlineData(0, "0")]
		[InlineData(1, "1")]
		[InlineData(123, "123")]
		public void Value_matches_ToString(short value, string expected)
		{
			var actual = new DepthLevel(value).ToString();
			Assert.Equal(expected, actual);
		}
	}
}
