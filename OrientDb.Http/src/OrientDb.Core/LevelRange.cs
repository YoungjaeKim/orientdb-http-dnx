using System;
using System.Collections.Generic;

namespace OrientDb
{
	public class LevelRange : IEqualityComparer<LevelRange>, IEquatable<LevelRange>
	{
		public ushort From { get; set; } = 0;
		public ushort To { get; set; } = ushort.MaxValue;
		public bool IsValid { get { return From.CompareTo(To) < 1; } }
		public bool IsAll { get { return From == 0 && To == ushort.MaxValue; } }

		public LevelRange()
		{
		}
		public LevelRange(ushort to)
		{
			From = 0;
			To = to;
		}
		public LevelRange(ushort from, ushort to)
		{
			From = from;
			To = to;
		}
		public LevelRange(string range)
		{
			if (range == null)
			{
				throw new ArgumentNullException(nameof(range));
			}
			var evaluation = range.Trim();
			if (evaluation == "" || evaluation == "*")
			{
				return;
			}

			var offset = evaluation.IndexOf("-");
			ushort from = 0;
			ushort to = 0;
			if (offset < 0)
			{
				if (!ushort.TryParse(evaluation, out from))
				{
					throw new ArgumentException($"Cannot parse range '{range}'.", nameof(range));
				}
				to = from;
			}
			else if (offset == 0)
			{
				from = 0;
				if (!ushort.TryParse(evaluation.Substring(1), out to))
				{
					throw new ArgumentException($"Cannot parse range '{range}'.", nameof(range));
				}
			}
			else if (offset == evaluation.Length - 1)
			{
				if (!ushort.TryParse(evaluation.Substring(0, offset), out from))
				{
					throw new ArgumentException($"Cannot parse range '{range}'.", nameof(range));
				}
				to = ushort.MaxValue;
			}
			else
			{
				if (!ushort.TryParse(evaluation.Substring(0, offset), out from))
				{
					throw new ArgumentException($"Cannot parse range '{range}'.", nameof(range));
				}
				if (!ushort.TryParse(evaluation.Substring(offset + 1), out to))
				{
					throw new ArgumentException($"Cannot parse range '{range}'.", nameof(range));
				}
			}
			From = from;
			To = to;
		}

		public bool Equals(LevelRange other)
		{
			if (ReferenceEquals(this, other)) return true;
			return From == other.From && To == other.To;
		}
		public override bool Equals(object obj)
		{
			var levelRange = (LevelRange)obj;
			if (levelRange == null)
			{
				return false;
			}
			return Equals(levelRange);
		}
		public override int GetHashCode()
		{
			return (From * 17) ^ (To * 17);
		}
		public bool Equals(LevelRange x, LevelRange y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null || y == null)
			{
				return false;
			}
			return x.Equals(y);
		}

		public int GetHashCode(LevelRange obj)
		{
			return obj.GetHashCode();
		}

		public override string ToString()
		{
			if (IsAll)
			{
				return "*";
			}
			if (From == To)
			{
				return $"{From}";
			}
			return $"{From}-" + ((To == ushort.MaxValue) ? "" : $"{To}");
		}
	}
}
