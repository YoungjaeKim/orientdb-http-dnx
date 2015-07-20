using System;
using Xunit;

namespace OrientDb.Tests
{
	public class FetchPlanTests
    {
		[Fact]
		public void New_one_is_all_first_level()
		{
			var value = new FetchPlan();
			Assert.True(value.Levels.IsAll);
			Assert.True(value.DepthMapping["*"].Value == 1);
		}

		[Fact]
		public void New_one_is_default()
		{
			var value = new FetchPlan();
			Assert.True(value.IsDefault);
		}

		[Fact]
		public void GuardClause()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var value = new FetchPlan(null);
			});
		}

		[Fact]
		public void Complex_fetch_plan()
		{
			var value = new FetchPlan("[3-5]in_*:-2 out_*:-1 myValue:1");
			Assert.Equal(3, value.Levels.From);
			Assert.Equal(5, value.Levels.To);
			Assert.Equal(3, value.DepthMapping.Count);
			Assert.Equal(DepthLevel.LevelType.Excluded, value.DepthMapping["in_*"].Level);
			Assert.Equal(DepthLevel.LevelType.Unlimited, value.DepthMapping["out_*"].Level);
			Assert.Equal(DepthLevel.LevelType.Level, value.DepthMapping["myValue"].Level);
			Assert.Equal(1, value.DepthMapping["myValue"].Value);
		}

		[Theory]
		[InlineData("*:0")]
		[InlineData("in_*:-2")]
		[InlineData("in_*:-2 out_*:-2")]
		[InlineData("in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[*]*:0")]
		[InlineData("[*]in_*:-2")]
		[InlineData("[*]in_*:-2 out_*:-2")]
		[InlineData("[*]in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[1]*:0")]
		[InlineData("[1]in_*:-2")]
		[InlineData("[1]in_*:-2 out_*:-2")]
		[InlineData("[1]in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[-1]*:0")]
		[InlineData("[1-]in_*:-2")]
		[InlineData("[0-5]in_*:-2 out_*:-2")]
		[InlineData("[3-5]in_*:-2 out_*:-2 myValue:-2")]
		public void Initialize_with_valid_fetch_plan_string(string input)
		{
			var value = new FetchPlan(input);
			Assert.NotNull(value);
		}

		[Theory]
		[InlineData("*:0", "*:0")]
		[InlineData("in_*:-2", "in_*:-2")]
		[InlineData("in_*:-2 out_*:-2", "in_*:-2 out_*:-2")]
		[InlineData("in_*:-2 out_*:-2 myValue:-2", "in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[*]*:0", "*:0")]
		[InlineData("[*]in_*:-2", "in_*:-2")]
		[InlineData("[*]in_*:-2 out_*:-2", "in_*:-2 out_*:-2")]
		[InlineData("[*]in_*:-2 out_*:-2 myValue:-2", "in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[1]*:0", "[1]*:0")]
		[InlineData("[1]in_*:-2", "[1]in_*:-2")]
		[InlineData("[1]in_*:-2 out_*:-2", "[1]in_*:-2 out_*:-2")]
		[InlineData("[1]in_*:-2 out_*:-2 myValue:-2", "[1]in_*:-2 out_*:-2 myValue:-2")]
		[InlineData("[-1]*:0", "[0-1]*:0")]
		[InlineData("[1-]in_*:-2", "[1-]in_*:-2")]
		[InlineData("[0-5]in_*:-2 out_*:-2", "[0-5]in_*:-2 out_*:-2")]
		[InlineData("[3-5]in_*:-2 out_*:-2 myValue:-2", "[3-5]in_*:-2 out_*:-2 myValue:-2")]
		public void Valid_fetch_plan_string_ToString(string input, string expected)
		{
			var value = new FetchPlan(input);
			Assert.Equal(expected, value.ToString());
		}

		[Theory]
		[InlineData("[")]
		[InlineData("[]")]
		[InlineData("[-]")]
		[InlineData("[*:-1")]
		[InlineData("[asf]*:-1")]
		[InlineData("[1234556789]*:-1")]
		[InlineData("[*-]*:-1")]
		[InlineData("[-*]*:-1")]
		[InlineData("[-]*:-1")]
		public void Invalid_level_throws_ArgumentException(string input)
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var value = new FetchPlan(input);
			});
		}

		[Theory]
		[InlineData("*:0 *:-1")]
		[InlineData("in_*:-2 in_*:-2")]
		[InlineData("[*]*:0 *:-1")]
		[InlineData("[*]in_*:-2 in_*:-2")]
		public void Duplicated_depth_throws_ArgumentException(string input)
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var value = new FetchPlan(input);
			});
		}
    }
}
