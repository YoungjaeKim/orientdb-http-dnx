using OrientDb.Utils;
using Xunit;

namespace OrientDb.Tests.Utils
{
	public class TextUtilsTests
    {
		[Theory]
		[InlineData("var a;", "var a;")]
        [InlineData("var b;// comment", "var b;")]
		[InlineData("var c;/*comment*/", "var c;")]
		[InlineData("var d;// comment /* co */", "var d;")]
		[InlineData("var e;// comment /*co", "var e;")]
		[InlineData("var f;// comment */", "var f;")]
		[InlineData(@"/*
		multiline comment
		multiline 2
		*/", "")]
		[InlineData("var g = \"// comment\";", "var g = \"// comment\";")]
		[InlineData("var h = \"/*comment*/\";", "var h = \"/*comment*/\";")]
		[InlineData("var i = \"// comment /* co */\";", "var i = \"// comment /* co */\";")]
		[InlineData("var j = \"// comment /*co\";", "var j = \"// comment /*co\";")]
		[InlineData("var k = \"// comment */\";", "var k = \"// comment */\";")]
        public void StripComments_strips_only_comments(string source, string expected)
		{
			var actual = TextUtils.StripComments(source);
			Assert.Equal(expected, actual);
		}
    }
}
