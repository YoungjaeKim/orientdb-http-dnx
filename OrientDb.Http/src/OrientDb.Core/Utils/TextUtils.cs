using System.Text.RegularExpressions;

namespace OrientDb.Utils
{
	public class TextUtils
    {
		private static Regex commentRegex = 
			new Regex(@"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/", RegexOptions.Compiled);

		public static string StripComments(string text)
		{
			return commentRegex.Replace(text, "$1");
		}
	}
}
