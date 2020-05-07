using System;
using System.Collections.Generic;

namespace Elma
{
	public static class Info
	{
		public static int NumberOfLevels = State.NumberOfLevels;

		public static IEnumerable<string> LevelNames =>
			Properties.Resources.LevelNames.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

		public const int StateFileSize = State.StateSize;
	}
}