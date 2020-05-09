using System;
using System.Collections.Generic;
using Elma.Properties;

namespace Elma
{
	public static class Info
	{
		public static string[] LevelNames =>
			Resources.LevelNames.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

		public static IReadOnlyCollection<byte> EmptyState => Resources.EmptyState;

		public const int MaxPlayerNameLength = State.MaxNameLength;
	}
}