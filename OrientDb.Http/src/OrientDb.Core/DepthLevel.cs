using System;

namespace OrientDb
{
	public struct DepthLevel : IEquatable<DepthLevel>
    {
		public static readonly DepthLevel Default = new DepthLevel(1);
		public static readonly DepthLevel Unlimited = new DepthLevel(-1);
		public static readonly DepthLevel Excluded = new DepthLevel(-2);

		public enum LevelType
		{
			Level,
			Unlimited,
			Excluded
		}

		public DepthLevel(short value)
		{
			Value = (value < -2) ? (short)-2 : value;
		}

		public short Value { get; }
		public LevelType Level
		{
			get
			{
				if (Value == -1)
				{
					return LevelType.Unlimited;
				}
				if (Value < -1)
				{
					return LevelType.Excluded;
				}
				return LevelType.Level;
			}
		}

		public bool Equals(DepthLevel other)
		{
			if (Level == LevelType.Level)
			{
				return Value == other.Value && other.Level == LevelType.Level;
			}
			else
			{
				return Level == other.Level;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is DepthLevel)
			{
				var depth = (DepthLevel)obj;
				return Equals(obj);
			}
			return false;
		}
		public override int GetHashCode()
		{
			return Value.GetHashCode();
        }

		public static bool operator ==(DepthLevel left, DepthLevel right)
		{
			if (ReferenceEquals(left, right))
			{
				return true;
			}
			return left.Equals(right);
		}

		public static bool operator !=(DepthLevel left, DepthLevel right)
		{
			return !(left == right);
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
