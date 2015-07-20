using System;
using System.Collections.Generic;

namespace OrientDb
{
	/// <summary>
	/// A class represents Record Id in OrientDB system.
	/// </summary>
	public class Rid : IEqualityComparer<Rid>, IEquatable<Rid>, IComparer<Rid>
	{
		static readonly char[] colon = new char[] { ':' };
		public static Rid Indeterminated { get { return new Rid(null); } }

		public Rid()
		{
		}
		public Rid(string rid)
		{
			short clusterId = -1;
			long position = -1;

			if (!string.IsNullOrWhiteSpace(rid)
				&& rid.StartsWith("#"))
			{
				var pair = rid.Substring(1).Split(colon, StringSplitOptions.RemoveEmptyEntries);
				if (pair.Length >= 2)
				{
					if (!short.TryParse(pair[0], out clusterId)) clusterId = -1;
					if (!long.TryParse(pair[1], out position)) position = -1;
                }
			}
			ClusterId = clusterId;
			Position = position;
		}
		public Rid(short clusterId, long position)
		{
			ClusterId = clusterId;
			Position = position;
		}

		public bool IsIndeterminated { get { return ClusterId < 0 || Position < 0; } }
        public short ClusterId { get; set; } = -1;
		public long Position { get; set; } = -1;

		public override int GetHashCode()
		{
			return (ClusterId * 17) ^ Position.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var rid = (Rid)obj;
			return Equals(rid);
		}

		public bool Equals(Rid other)
		{
			return IsIndeterminated == other.IsIndeterminated
				&& ClusterId == other.ClusterId
				&& Position == other.Position;
		}

		public bool Equals(Rid x, Rid y)
		{
			return x.IsIndeterminated == y.IsIndeterminated
				&& x.ClusterId == y.ClusterId
				&& x.Position == y.Position;
		}

		public int GetHashCode(Rid obj)
		{
			return obj.GetHashCode();
		}

		public int Compare(Rid x, Rid y)
		{
			var compare = x.IsIndeterminated.CompareTo(y.IsIndeterminated);
			if (compare == 0)
			{
				compare = x.ClusterId.CompareTo(y.ClusterId);
				if (compare == 0)
				{
					compare = x.Position.CompareTo(y.Position);
				}
			}
			return compare;
		}

		public override string ToString()
		{
			return $"#{ClusterId}:{Position}";
		}

		public static bool operator ==(Rid left, Rid right)
		{
			if (ReferenceEquals(left, right))
			{
				return true;
			}
			return left.Equals(right);
		}

		public static bool operator !=(Rid left, Rid right)
		{
			return !(left == right);
		}
	}
}
