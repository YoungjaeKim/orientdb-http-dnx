using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OrientDb
{
	public class FetchPlan
    {
		private static readonly char[] blank = new char[] { ' ' };
		private static readonly char[] colon = new char[] { ':' };

		// samples: http://regexr.com/3bdt9
		// explain: http://goo.gl/lgEzVu
		private static readonly Regex fetchPlanRegex = new Regex(
			@"^(?:\[(\*|(?:-?\d{1,3})|(?:\d{1,3}-\d{0,3}))\])?(?:([_\w\d\@]+(?:\.[_\w\d\@])*\*?|\*):(-1|-2|\d{1,3}))(?: ([_\w\d\@]+(?:\.[_\w\d\@])*\*?|\*):(-1|-2|\d{1,3}))*$"
			, RegexOptions.Compiled | RegexOptions.Singleline);

		public FetchPlan()
		{
		}
		public FetchPlan(string fetchPlan)
		{
			if (fetchPlan == null)
			{
				throw new ArgumentNullException(nameof(fetchPlan));
			}

			var match = fetchPlanRegex.Match(fetchPlan);
			if (!match.Success)
			{
				throw new ArgumentException($"Invalid fetch plan text format. '{fetchPlan}'", nameof(fetchPlan));
			}

			var dic = new Dictionary<string, DepthLevel>();
			var group1 = match.Groups[1];
			if (group1.Success)
			{
				Levels = new LevelRange(group1.Value);
			}
			var group2 = match.Groups[2];
			var group3 = match.Groups[3];
			dic.Add(group2.Value, new DepthLevel(short.Parse(group3.Value)));
			var group4 = match.Groups[4];
			var group5 = match.Groups[5];
			if (group4.Success && group5.Success)
			{
				for (int i = 0; i < group4.Captures.Count; i++)
				{
					var key = group4.Captures[i].Value;
					var value = group5.Captures[i].Value;
					dic.Add(key, new DepthLevel(short.Parse(value)));
				}
			}
			DepthMapping = dic;
		}

		public LevelRange Levels { get; set; } = new LevelRange();
		public IDictionary<string, DepthLevel> DepthMapping { get; set; } = new Dictionary<string, DepthLevel> { { "*", DepthLevel.Default } };
		public bool IsDefault { get { return Levels.IsAll && DepthMapping.Count == 1 && DepthMapping["*"] == DepthLevel.Default; } }

		public override string ToString()
		{
			var result = "";
			if (!Levels.IsAll)
			{
				result = $"[{Levels}]";
			}
			return result + string.Join(" ", DepthMapping.Select(m => $"{m.Key}:{m.Value}"));
		}
	}
}
